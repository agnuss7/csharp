using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentScript : MonoBehaviour {
	public string comment;
	private GameObject CommentController;
	void Awake(){
		CommentController = GameObject.Find ("CommentSpace");
	}
	public void Comment() {
		CommentController.SetActive (true);
		CommentController.SendMessage ("StartComment",comment);
	}
	

}
