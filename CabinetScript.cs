using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetScript : MonoBehaviour {
	public GameObject Inventory;
	public GameObject CommentControl;
	private int wasclic=8;

	void Operate(string l){
		if (l == "" && BoolsInts.boolies[wasclic]==false) {
			Inventory.SendMessage ("AddItem", "syringe");
			CommentControl.SendMessage ("StartComment", "In the cabinet i found... a syringe");
			BoolsInts.boolies[wasclic] = true;
		} else if (l != "") {
			CommentControl.SendMessage ("InvalidComment");
		} else {
			CommentControl.SendMessage ("StartComment", "I already looked through the cabinet.");
		}
	}

}
