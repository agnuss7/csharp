using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PickUpItem{
	public Sprite image;
	public string name;
	public string comment;
}

[System.Serializable]
public class combination {
	public string first;
	public bool firstDestroy;
	public string second;
	public bool secondDestroy;
	public string resultName;
	public string Comment;

	public bool combExists(string a, string b){
		if ((a == first && b == second) || (a == second && b == first)) {
			return true;
		} else {
			return false;
		}
	}

}

public class InventoryScript : MonoBehaviour {
	public GameObject panel;
	public string combining="";
	public GameObject Controller;
	public GameObject CommentControl;
	public GameObject prefab;
	public int countOfCombinations;
	public combination[] Combinations;
	public PickUpItem[] WorldPickUpItems;
	public GameObject mouse;

	public GameObject closeup4book;

	// Use this for initialization
	void Start () {
		
		//panel = transform.GetChild (0).gameObject;
		if (Controller == null) {
			Controller = GameObject.Find ("Controller").gameObject;
		}
		if (CommentControl == null) {
			CommentControl = GameObject.Find ("CommentSpace").gameObject;
		}
		if (mouse == null) {
			mouse = GameObject.Find ("MouseText").gameObject;
		}
		mouse.SendMessage ("NoImage");
	}




	int countObjects(){
		return transform.GetChild (0).childCount;
	}
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.y > Camera.main.pixelHeight * 0.9) {
			if (panel.transform.localPosition.y > 220f) {
				panel.transform.position = new Vector2 (panel.transform.position.x, panel.transform.position.y - 0.12f);
			} else {
				if (Input.GetMouseButtonDown (0)) {
					Controller.SetActive (false);
					Controller.SetActive (true);
				}
			}
			if (/*countObjects()>14 && */Input.mousePosition.x>Camera.main.pixelWidth*0.85) {
				if (panel.transform.localPosition.x > -300f) {
					panel.transform.localPosition = new Vector2 (panel.transform.localPosition.x - 3f, panel.transform.localPosition.y);
				} 
			}
			if (Input.mousePosition.x<Camera.main.pixelWidth*0.15) {
				if (panel.transform.localPosition.x<0f) {
					panel.transform.localPosition = new Vector2 (panel.transform.localPosition.x + 3f, panel.transform.localPosition.y);
				} 
			}
		} else {
			if (panel.transform.localPosition.y < 320f) {
				transform.GetChild (0).position = new Vector2 (transform.GetChild (0).position.x, transform.GetChild (0).position.y + 0.12f);
			}
		}
			

	}

	/*void AddItem(PickUpItem item){
		GameObject New=Instantiate (prefab, panel.transform);
		New.name = item.name;
		New.GetComponent<Image> ().sprite = item.image;
		New.GetComponent<PickUpItemScript> ().comment = item.comment;
	}*/
	void AddItem(string item){
		GameObject New=Instantiate (prefab, panel.transform);
		New.name = item;
		foreach (PickUpItem x in WorldPickUpItems) {
			if (x.name == item) {
				New.GetComponent<Image> ().sprite = x.image;
				New.GetComponent<PickUpItemScript> ().comment = x.comment;
				break;
			}
		}
	}


	void DeleteItem(string item){
		for (int i = 0; i < panel.transform.childCount; i++) {
			if (panel.transform.GetChild(i).name == item) {
				Destroy (panel.transform.GetChild(i).gameObject);
				break;
			}
		}
	}

	void CombineItems(combination C){
		if (C.firstDestroy) {
			for (int i = 0; i < panel.transform.childCount; i++) {
				if (C.first == panel.transform.GetChild (i).name) {
					Destroy (panel.transform.GetChild (i).gameObject);
					break;
				}
			}
		}
		if (C.secondDestroy) {
			for (int i = 0; i < panel.transform.childCount; i++) {
				if (C.second == panel.transform.GetChild (i).name) {
					Destroy (panel.transform.GetChild (i).gameObject);
					break;
				}
			}
		}
		AddItem(C.resultName);
		CommentControl.SendMessage ("StartComment",C.Comment);
	}

	void clickedItem(string N){
		if (combining == "") {
			combining = N;
			if (Controller.activeSelf) {
				Controller.SendMessage ("OperateWith", N);
			}
			mouse.SendMessage ("VisibleImage");
		} else {
			bool temp = false;
			for (int i = 0; i < countOfCombinations; i++) {
				if (Combinations [i].combExists (combining, N)) {
					CombineItems (Combinations[i]);
					temp = true;
					break;
				}
			}
			if (!temp) {
				CommentControl.SendMessage ("InvalidComment");
			}
			combining = "";
			if (Controller.activeSelf) {
				Controller.SendMessage ("OperateWith", "");
			}
			mouse.SendMessage ("NoImage");
		}
	}

	void CombItemNull(){
		combining = "";
		mouse.SendMessage ("NoImage");
	}

	IEnumerator RightClickFunctionality(string s){
		if (s == "book") {
			yield return new WaitForSeconds(2);
			closeup4book.SetActive (true);
		}
	}
}
