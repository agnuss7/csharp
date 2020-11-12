using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookCaseScript : MonoBehaviour {
	private int open=6;
	private float coordinatex;
	public GameObject CommentController;
	public GameObject passage;
	// Use this for initialization
	void Start () {
		coordinatex = gameObject.transform.position.x - 4.2f;
		CommentController = GameObject.Find ("CommentSpace").gameObject;
		if (BoolsInts.boolies[open]) {
			gameObject.transform.position = new Vector2 (coordinatex,gameObject.transform.position.y);
		}
	}
	void Operate(string s){
		if (!BoolsInts.boolies[open]) {
			if (s == "") {
				CommentController.SendMessage ("StartComment", "I rummaged through the books hoping it would open a secret passage, but no cigar...");
			} else {
				CommentController.SendMessage ("InvalidComment");
			}
		}
	}
	// Update is called once per frame
	void Update(){
		if (BoolsInts.boolies [open] && transform.position.x > coordinatex) {
			transform.position = new Vector2 (transform.position.x-0.1f,transform.position.y);
		}
	}
	void OpenUp () {
		BoolsInts.boolies[open] = true;
	}
}
