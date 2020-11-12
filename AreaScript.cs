using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nodes {
	bool isValid;
	Vector2 vertex;
	float gCost;
	float hCost;
	float fCost;
	int x;
	int y;
	public Nodes (Vector2 v){
		vertex = v;
		isValid = true;
		gCost = 1000f;
		hCost = 1000f;
		fCost = 1000f;
		x = -1;
		y = -1;
	}
	public void SetFalse(){
		isValid = false;
	}
	public Vector2 givePoint(){
		return vertex;
	}
	public bool Valid(){
		return isValid;
	}
	public string give(){
		return vertex.ToString () + isValid.ToString ();
	}
	public void Reset(){
		gCost = 1000f;
		hCost = 1000f;
		fCost = 1000f;
		x = -1;
		y = -1;
	}
	public void setParent(int xx,int yy){
		x = xx;
		y = yy;
	}
	public void setAll(int sX,int sY){
		fCost = 0f;
		gCost = 0f;
		hCost = 0f;
		x = sX;
		y = sY;
	}
	public float giveG(){
		return gCost;
	}
	public float giveF(){
		return fCost;
	}
	public int giveX(){
		return x;
	}
	public int giveY(){
		return y;
	}
	public void setCosts(float f,float g,float h,int pX, int pY){
		gCost = g;
		fCost = f;
		hCost = h;
		x = pX;
		y = pY;
	}
}

public class AreaScript : MonoBehaviour {
	public int width;
	public int height;
	public float size=0.5f;
	public Nodes[,] Grid;
	// Use this for initialization
	private bool checkPoint (Vector2 vv){
		bool check = true;
		int count=transform.childCount;
			for (int i = 1; i < count; i++) {
			if (transform.GetChild (i).Find ("ObjectFloor")!=null) {
				Collider2D child = transform.GetChild (i).Find ("ObjectFloor").GetComponent<Collider2D> ();
				if (child.OverlapPoint (vv)) {
					check = false;
				}
			}
		}


		int decorcount = transform.GetChild (0).GetChild(0).childCount;
		for (int i = 0; i < decorcount; i++) {
			if (transform.GetChild (0).GetChild(0).GetChild(i).GetComponent<Collider2D>()!=null) {
				if (transform.GetChild (0).GetChild(0).GetChild(i).GetComponent<Collider2D>().OverlapPoint (vv)) {
					check = false;
				}
			}
		}
		return check;
	}


	public void Redo() {
		Collider2D floor = transform.GetChild (0).GetChild(1).GetComponent<Collider2D> ();
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if(!floor.OverlapPoint(Grid[i,j].givePoint()) || !checkPoint(Grid[i,j].givePoint())){
					Grid [i, j].SetFalse();
				}
			}
		}
	}

	void Awake () {
		Collider2D floor = transform.GetChild (0).GetChild(1).GetComponent<Collider2D> ();
		width = (int)(floor.bounds.size.x*2)+1;
		height = (int)(floor.bounds.size.y*2)+1;
		float startx = floor.bounds.min.x;
		float starty = floor.bounds.max.y;
		Grid = new Nodes[width,height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Vector2 vv = new Vector2 (startx + size*i, starty - size*j);
				Grid [i, j] = new Nodes (vv);
				if(!floor.OverlapPoint(vv) || !checkPoint(vv)){
					Grid [i, j].SetFalse();
				}
			}
		}
	}





}
