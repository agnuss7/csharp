using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct pPair{
	public int x;
	public int y;
	public float f;
	public pPair(int a, int b, float c){
		x = a;
		y = b;
		f = c;
	}
}

public class ObjectToWalkTo{
	protected bool isClicked=false;
	protected Vector2 ClosePoint;
	protected float radius=0.5f;

	public Vector2 givePoint(){
		return ClosePoint;
	}
	public void setClicked(bool a){
		isClicked = a;
	}
	public bool isNear(Vector2 x){
		if (isClicked == true && Vector2.Distance (x, ClosePoint) < radius) {
			return true;
		} else
			return false;
	}
	public bool inProgress(){
		return isClicked;
	}
}

public class ActiveArea {
	private Area[] Areas;
	private int current;
	public int size;
	public ActiveArea(Area[] a, int b, int c){
		Areas = a;
		size = b;
		current = c;
	}
	public Area CurrentArea(){
		return Areas[current];
	}
	public int currentAreaIndex(){
		return current;
	}
	public void setArea(GameObject x){
		for (int i = 0; i < size; i++) {
			if (Areas[i].giveObject() == x) {
				Areas [i].setActive(true);
				current = i;
			} else {
				Areas [i].setActive(false);
			}
		}
	}
}

// Daiktu vietovej klases

public class Operable:ObjectToWalkTo{
	private GameObject gObject;
	public Operable(GameObject a){
		gObject = a;
		ClosePoint = a.transform.Find("Enter").position;
	}
	public GameObject giveOperableObject(){
		return gObject;
	}
}

public class Door : ObjectToWalkTo{
	private GameObject D;
	private GameObject TargetArea;
	private Vector2 TargetPoint;
	public Door (GameObject s){
		D = s;
		DoorScript sk = s.GetComponent<DoorScript> ();
		TargetArea = sk.TargetArea;
		TargetPoint = sk.TargetDoor.transform.GetChild(0).position;
		ClosePoint = s.transform.GetChild (0).position;
	}

	public Vector2 giveTargetPoint (){
		return TargetPoint;
	}
	public GameObject giveTargetArea(){
		return TargetArea;
	}
	public GameObject giveDoorObject(){
		return D;
	}
}

public class Area {
	private GameObject A;
	private int width;
	private int height;
	private Nodes[,] Grid;

	private float minX;
	private float minY;
	private float maxX;
	private float maxY;

	private int DoorCount; 
	private Door[] Doors;

	private int OperableCount;
	private Operable[] operables;

	public Area(GameObject S) {
		A = S;
		AreaScript sk = S.GetComponent<AreaScript> ();
		width = sk.width;
		height = sk.height;
		Grid = sk.Grid;
		minX = A.GetComponent<Collider2D>().bounds.min.x;
		maxY = A.GetComponent<Collider2D>().bounds.max.y;
		minY = A.GetComponent<Collider2D>().bounds.min.y;
		maxX = A.GetComponent<Collider2D>().bounds.max.x;
		DoorCount = A.transform.GetChild (0).childCount-2;
		OperableCount = A.transform.childCount - 1;
		Doors=new Door[DoorCount];
		operables=new Operable[OperableCount];
		for (int i = 0; i < DoorCount; i++) {
			Doors [i] = new Door(A.transform.GetChild (0).GetChild (i+2).gameObject);
		}
		for (int i = 0; i < OperableCount; i++) {
			operables [i] = new Operable (A.transform.GetChild(i+1).gameObject);
		}
	}
	public void RedoTheGrid(){
		A.SendMessage ("Redo");
		Grid = A.GetComponent<AreaScript> ().Grid;
	}
	public int giveOperablesCount(){
		return OperableCount;
	}

	public Operable GiveOperable(int x){
		return operables [x];
	}
	// 
	public int giveDoorCount(){
		return DoorCount;
	}
	public Door GiveDoor(int x){
		return Doors[x];
	}


	//
	public GameObject giveObject(){
		return A;
	}

	public float giveMinX(){
		return minX;
	}
	public float giveMinY(){
		return minY;
	}
	public float giveMaxX(){
		return maxX;
	}
	public float giveMaxY(){
		return maxY;
	}
	public float calcH(int sX, int sY, int dX,int dY){
		return ((sX-dX)*(sX-dX)+(sY-dY)*(sY-dY));
	}
	public void setActive(bool s){
		A.SetActive (s);
	}
	void Reset() {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Grid [i, j].Reset();
			}
		}
	}
	public Stack<Vector2> Pathfinder(Vector2 src, Vector2 fin){
		Reset ();
		int destX=0;
		int destY=0;
		int srcX=0;
		int srcY=0;
		float minsrc=10000f;
		float minfin=10000f;
		bool[,] closedList=new bool[width,height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (Grid [i, j].Valid ()) {
					if (Vector2.Distance (fin, Grid [i, j].givePoint ()) < minfin) {
						destX = i;
						destY = j;
						minfin = Vector2.Distance (fin, Grid [i, j].givePoint ());
					}
					if (Vector2.Distance (src, Grid [i, j].givePoint ()) < minsrc) {
						srcX = i;
						srcY = j;
						minsrc = Vector2.Distance (src, Grid [i, j].givePoint ());
					}
				}
				closedList [i, j] = false;
			}
		}
		if (destX == srcX && destY == srcY) {
			Stack<Vector2> d=new Stack<Vector2>();
			d.Push(Grid[destX,destY].givePoint());
			return d;
		}
		 
		Grid [srcX, srcY].setAll (srcX,srcY);
		List<pPair> OpenList = new List<pPair> ();
		OpenList.Add(new pPair(srcX,srcY,0f));

		int curX, curY;
		int[] gothrough = {-1,0,1,0,0,1,0,-1,-1,1,-1,-1,1,1,1,-1};
		float[] addG = { 1f,1f,1f,1f,1.314f,1.314f,1.314f,1.314f};
		while (OpenList.Count > 0) {
			float minf = 10000f;
			int minif = 0;
			for (int a = 0; a < OpenList.Count; a++) {
				if (OpenList [a].f < minf) {
					minf = OpenList [a].f;
					minif = a;
				}
			}
			curX = OpenList [minif].x;
			curY = OpenList [minif].y;
			OpenList.RemoveAt (minif);
			closedList [curX, curY] = true;
			if (curX == destX && curY == destY) {
				//
				//rasta
				//
				int fx, fy;
				fx = destX;
				fy = destY;
				Stack<Vector2> final = new Stack<Vector2> ();
				while (!(fx == srcX && fy == srcY)) {
					final.Push (Grid[fx,fy].givePoint());
					int newx = Grid [fx, fy].giveX ();
					int newy = Grid [fx, fy].giveY ();
					fx = newx;
					fy = newy;
				}
				return final;
			}

			// einant per kaimynus
			for (int o = 0; o < 16; o += 2) {
				if ((curX + gothrough [o]) >= 0 && (curY + gothrough [o + 1]) >= 0 && (curX + gothrough [o]) < width && (curY + gothrough [o + 1]) < height) {
					if (Grid [curX + gothrough [o], curY + gothrough [o + 1]].Valid ()){
					if (closedList [curX + gothrough [o], curY + gothrough [o + 1]] == false) {
						float gNew = Grid [curX, curY].giveG () + addG [o / 2];
						float hNew = calcH (curX + gothrough [o], curY + gothrough [o + 1], destX, destY);
						float fNew = gNew + hNew;

						bool isinopen = false;
						for (int q = 0; q < OpenList.Count; q++) {
							if (OpenList [q].x == curX + gothrough [o] && OpenList [q].y == curY + gothrough [o + 1]) {
									isinopen = true;
									if (gNew < Grid [curX + gothrough [o], curY + gothrough [o + 1]].giveG ()) {
										OpenList.RemoveAt (q);
										isinopen = false;
								}
							}
						}
						// 
						if (!isinopen) {
							OpenList.Add (new pPair (curX + gothrough [o], curY + gothrough [o + 1], fNew));
							Grid [curX + gothrough [o], curY + gothrough [o + 1]].setCosts (fNew, gNew, hNew, curX, curY);
						}
						//

					}
				}
				}
			}
		}
		return new Stack<Vector2> ();
	}
}
public class ControllerScript : MonoBehaviour {
	public List<string> deleteditems;
	void recordDeletedItem(string a){
		deleteditems.Add (a);
	}
	private bool wasClicked;
	public GameObject Emeth;
	public GameObject[] AreaObjects;
	public GameObject DialogueController;
	public GameObject CommentController;
	public GameObject Inventory;
	public GameObject Menu;
	private Area Area1Class;
	private Area Area2Class;
	private Area[] AreaClasses;
	public ActiveArea activeArea;
	private ObjectToWalkTo temporary;
	public int OnLoadActiveArea;

	public string operateObject;

	void Start () {
		if (deleteditems.Count > 0) {
			Queue<GameObject> que=new Queue<GameObject>();
			foreach (GameObject ar in AreaObjects) {
				for (int i = 1; i < ar.transform.childCount; i++) {
					if (deleteditems.Contains (ar.transform.GetChild (i).name)) {
						que.Enqueue(ar.transform.GetChild (i).gameObject);
					}
				}
			}
			while (que.Count > 0) {
				GameObject tempor = que.Dequeue ();
				Destroy(tempor);
			}
		}
		AreaClasses=new Area[AreaObjects.Length];
		for (int i = 0; i < AreaClasses.Length; i++) {
			AreaClasses [i] = new Area (AreaObjects[i]);
		}
		//Area1Class = new Area (Area1);
		//Area2Class = new Area (Area2);
		//Area[] temp = { Area1Class,Area2Class};
		activeArea = new ActiveArea (AreaClasses,AreaClasses.Length,OnLoadActiveArea);
		temporary = new ObjectToWalkTo ();
		wasClicked = false;
		operateObject="";
		if (Inventory == null) {
			Inventory = GameObject.Find ("Inventory").gameObject;
		}
		Menu.SetActive (false);
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Menu.SetActive (true);
			Emeth.SendMessage ("GiveCoordinates", new Stack<Vector2> ());
			Inventory.SetActive (false);
			gameObject.SetActive (false);
		}
		if (Input.GetMouseButtonDown(0)) {
			Vector2 Position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			for (int i = 0; i < activeArea.CurrentArea ().giveDoorCount (); i++) {
				if (activeArea.CurrentArea ().GiveDoor (i).giveDoorObject ().GetComponent<Collider2D> ()!=null && activeArea.CurrentArea ().GiveDoor (i).giveDoorObject ().GetComponent<Collider2D> ().OverlapPoint (Position)) {
						temporary.setClicked (false);
						activeArea.CurrentArea ().GiveDoor (i).setClicked (true);
						temporary = activeArea.CurrentArea ().GiveDoor (i);
						Emeth.SendMessage ("GiveCoordinates", activeArea.CurrentArea ().Pathfinder (Emeth.transform.position, activeArea.CurrentArea ().GiveDoor (i).givePoint ()));
						wasClicked = true;
						operateObject = "";
						Inventory.SendMessage ("CombItemNull");
						break;
				}
			}

			for (int i = 0; i < activeArea.CurrentArea().giveOperablesCount (); i++) {
				if (activeArea.CurrentArea ().GiveOperable (i).giveOperableObject()!=null && activeArea.CurrentArea ().GiveOperable (i).giveOperableObject ().GetComponent<Collider2D> ()!=null && activeArea.CurrentArea ().GiveOperable (i).giveOperableObject ().GetComponent<Collider2D> ().OverlapPoint (Position)) {
					temporary.setClicked (false);
					activeArea.CurrentArea ().GiveOperable (i).setClicked (true);
					temporary = activeArea.CurrentArea ().GiveOperable (i);
					Emeth.SendMessage("GiveCoordinates", activeArea.CurrentArea().Pathfinder (Emeth.transform.position,activeArea.CurrentArea ().GiveOperable (i).givePoint()));
					wasClicked = true;
					break;
				}
			}
			if (operateObject!="" && Emeth.GetComponent<Collider2D> ().OverlapPoint (Position)) {
				temporary.setClicked (false);
				Emeth.SendMessage("GiveCoordinates", new Stack<Vector2> ());
				Emeth.SendMessage ("Operate",operateObject);
				wasClicked = true;
				operateObject = "";
				Inventory.SendMessage ("CombItemNull");
			}

			if (wasClicked == false) {
				Emeth.SendMessage ("GiveCoordinates", activeArea.CurrentArea ().Pathfinder (Emeth.transform.position, Position));
				temporary.setClicked (false);
				operateObject = "";
				Inventory.SendMessage ("CombItemNull");
			}
			wasClicked = false;
		}


		if (temporary.isNear (Emeth.transform.position)) {
			for (int i = 0; i < activeArea.CurrentArea ().giveDoorCount (); i++) {
				Door tt = activeArea.CurrentArea ().GiveDoor (i);
				if (tt.isNear(Emeth.transform.position)) {
					if (BoolsInts.boolies[tt.giveDoorObject ().GetComponent<DoorScript> ().unlocked]) {
						Emeth.SendMessage ("GiveCoordinates", new Stack<Vector2> ());
						Vector2 teleport = tt.giveTargetPoint ();
						activeArea.setArea (tt.giveTargetArea ());
						Emeth.transform.position = teleport;
						if (Emeth.transform.position.x < tt.giveDoorObject ().transform.position.x && Emeth.transform.localScale.x < 0) {
							Emeth.transform.localScale = new Vector3 (Emeth.transform.localScale.x * -1, Emeth.transform.localScale.y, Emeth.transform.localScale.z);
						} else if (Emeth.transform.position.x > tt.giveDoorObject ().transform.position.x && Emeth.transform.localScale.x > 0) {
							Emeth.transform.localScale = new Vector3 (Emeth.transform.localScale.x * -1, Emeth.transform.localScale.y, Emeth.transform.localScale.z);
						}
					} else {
						CommentController.SetActive (true);
						CommentController.SendMessage ("StartComment","It's locked.");
					}
					break;
				}
			}
			for (int i = 0; i < activeArea.CurrentArea ().giveOperablesCount (); i++) {
				Operable tt = activeArea.CurrentArea ().GiveOperable (i);
				if (tt.isNear(Emeth.transform.position) && tt.giveOperableObject()!=null) {
					Emeth.SendMessage ("GiveCoordinates", new Stack<Vector2>());
					if (Emeth.transform.position.x < tt.giveOperableObject ().transform.position.x && Emeth.transform.localScale.x > 0) {
						Emeth.transform.localScale = new Vector3 (Emeth.transform.localScale.x * -1, Emeth.transform.localScale.y, Emeth.transform.localScale.z);
					} else if (Emeth.transform.position.x > tt.giveOperableObject ().transform.position.x && Emeth.transform.localScale.x < 0) {
						Emeth.transform.localScale = new Vector3 (Emeth.transform.localScale.x * -1, Emeth.transform.localScale.y, Emeth.transform.localScale.z);
					}
					Inventory.SendMessage ("CombItemNull");
					tt.giveOperableObject ().SendMessage ("Operate",operateObject);
					operateObject = "";
					break;
				}
			}
			temporary.setClicked (false);
		}

		// examining

		if (Input.GetMouseButtonDown (1)) {
			Vector2 Position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			for (int i = 0; i < activeArea.CurrentArea ().giveDoorCount (); i++) {
				if (activeArea.CurrentArea ().GiveDoor (i).giveDoorObject ().GetComponent<Collider2D> ().OverlapPoint (Position)) {
					Emeth.SendMessage ("GiveCoordinates", new Stack<Vector2>());
					activeArea.CurrentArea ().GiveDoor (i).giveDoorObject().SendMessage("Comment");
					break;
				}
			}

			for (int i = 0; i < activeArea.CurrentArea().giveOperablesCount (); i++) {
				if (activeArea.CurrentArea ().GiveOperable (i).giveOperableObject()!=null && activeArea.CurrentArea ().GiveOperable (i).giveOperableObject ().GetComponent<Collider2D> ().OverlapPoint (Position)) {
					Emeth.SendMessage ("GiveCoordinates", new Stack<Vector2>());
					activeArea.CurrentArea ().GiveOperable (i).giveOperableObject().SendMessage("Comment");
					break;
				}
			}



		}




		// kamera

		Camera.main.transform.position = new Vector3(Emeth.transform.position.x,Emeth.transform.position.y+2.5f,Camera.main.transform.position.z);

		if (Camera.main.transform.position.x+Camera.main.orthographicSize*Camera.main.aspect>=activeArea.CurrentArea().giveMaxX()) {
			Camera.main.transform.position = new Vector3(activeArea.CurrentArea().giveMaxX()-Camera.main.orthographicSize*Camera.main.aspect,Camera.main.transform.position.y,Camera.main.transform.position.z);
		} else if (Camera.main.transform.position.x-Camera.main.orthographicSize*Camera.main.aspect<=activeArea.CurrentArea().giveMinX()) {
			Camera.main.transform.position = new Vector3(activeArea.CurrentArea().giveMinX()+Camera.main.orthographicSize*Camera.main.aspect,Camera.main.transform.position.y,Camera.main.transform.position.z);
		}
			

		if (Camera.main.transform.position.y+Camera.main.orthographicSize>=activeArea.CurrentArea().giveMaxY()) {
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,activeArea.CurrentArea().giveMaxY()-Camera.main.orthographicSize,Camera.main.transform.position.z);
		} else if (Camera.main.transform.position.y-Camera.main.orthographicSize<=activeArea.CurrentArea().giveMinY()) {
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,activeArea.CurrentArea().giveMinY()+Camera.main.orthographicSize,Camera.main.transform.position.z);
		}



		
	}

	void OperateWith(string N=""){
		operateObject = N;
	}

	void RedoGridInArea(int a){
		AreaClasses [a].RedoTheGrid ();
	}
}
