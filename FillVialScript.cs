using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillVialScript : MonoBehaviour {
	public GameObject Inventory;
	public GameObject CommentControl;
	public PickUpItem ret;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Operate (string a) {
		if (a == "empty vial") {
			Inventory.SendMessage ("DeleteItem","empty vial");
			Inventory.SendMessage ("AddItem",ret.name);
			CommentControl.SendMessage ("StartComment","I turned the tap and filled the vial with some strange liquid.");
		}
	}
}
