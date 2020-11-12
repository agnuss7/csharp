using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUpScript : MonoBehaviour {
	public GameObject CommentControl;
	public GameObject Inventory;
	public string pickitem;
	//public PickUpItem pickup;
	// Use this for initialization
	void Awake(){
		if (CommentControl == null) {
			CommentControl = GameObject.Find ("CommentSpace").gameObject;
		}
		if (Inventory == null) {
			Inventory = GameObject.Find ("Inventory").gameObject;
		}
		/*
		if (pickup.name == "") {
			pickup.name = name;
		}
		if (pickup.image == null) {
			pickup.image = GetComponent<SpriteRenderer> ().sprite;
		}*/
		if (pickitem == "" || pickitem==null) {
			pickitem = name;
		}
	}
	public void Operate () {
		CommentControl.SetActive (true);
		CommentControl.SendMessage ("StartComment","Picked up "+pickitem+".");
		Inventory.SendMessage("AddItem",pickitem);
		GameObject.Find("Controller").SendMessage ("recordDeletedItem",name);
		Destroy (gameObject);
		//gameObject.SetActive(false);
	}

}
