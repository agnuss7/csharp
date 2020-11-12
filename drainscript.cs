using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class drainscript : MonoBehaviour {
	public GameObject comm;
	public GameObject Inventory;
	private string[] vials = { "black vial","purple vial","yellow vial"};
	// Use this for initialization
	void Operate (string op) {
		if (op == "") {

		} else if (Array.IndexOf (vials, op) > -1) {
			Inventory.SendMessage ("DeleteItem",op);
			Inventory.SendMessage ("AddItem","empty vial");
			comm.SendMessage ("StartComment","Down the drain...");
		} else if (op == "blood vial") {
			comm.SendMessage ("StartComment","My blood is too precious to just throw away.");
		} else {
			comm.SendMessage ("InvalidComment");
		}
	}
	
}
