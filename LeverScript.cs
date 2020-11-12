using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {
	public GameObject Door;
	public GameObject CommentController;
	public GameObject Inventory;
	private int wasActivated=2;
	private int withTriangle=3;
	// Use this for initialization
	void Start(){
		if (Inventory == null) {
			Inventory = GameObject.Find ("Inventory").gameObject;
		}
	}
	public void Operate (string op) {
		if (op == "") {
			GetComponent<Animator> ().Play ("lever_down");
			if (BoolsInts.boolies[withTriangle]) {
				BoolsInts.boolies[Door.GetComponent<DoorScript> ().unlocked] = true;
				if (!BoolsInts.boolies[wasActivated]) {
					BoolsInts.boolies[wasActivated] = true;
					CommentController.SetActive (true);
					CommentController.SendMessage ("StartComment", "There was a clicking.");
				}
			}
				
		} else if (op == "triangle shape") {
			BoolsInts.boolies[withTriangle] = true;
			CommentController.SendMessage ("StartComment","I inserted the triangle shape into the hole on the lever.");
			Inventory.SendMessage ("DeleteItem","triangle shape");

		} else {
			CommentController.SendMessage ("InvalidComment");
		}
	} 


}
