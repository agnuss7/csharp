using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpScript : MonoBehaviour {
	public GameObject Comment;
	public GameObject Controller;
	public GameObject OpenUp;
	void Start(){
		if (Comment == null) {
			Comment = GameObject.Find ("CommentSpace").gameObject;
		}
		if (Controller == null) {
			Controller = GameObject.Find ("Controller").gameObject;
		}
	}
	public void Operate (string op) {
		if (op == "") {
			Controller.SetActive (false);
			OpenUp.SetActive (true);
		} else {
			Comment.SendMessage ("InvalidComment");
		}
	}
}
