using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentWindowScript : MonoBehaviour {
	public GameObject Emeth;
	public GameObject Inventory;
	private float time;
	private Color v=Color.black;
	private string[] InvalidComments;
	private int cnt;
	void Start(){
		time = 0f;
		v.a = 0;
		cnt = 3;
		InvalidComments = new string[]{"That won't work.","That's not right.","There's no effect."};

	}
	public void StartComment (string c) {
		v.a = 0;
		transform.GetChild (0).GetComponent<Text> ().text = c;
		Color f=Color.black;
		f.a = 255;
		transform.GetChild (0).GetComponent<Text> ().color=f;
		time = 2f;
	}

	void InvalidComment(){
		v.a = 0;
		transform.GetChild (0).GetComponent<Text> ().text = InvalidComments[Random.Range(0,cnt)];
		Color f=Color.black;
		f.a = 255;
		transform.GetChild (0).GetComponent<Text> ().color=f;
		time = 2f;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (Emeth.transform.position.x,Emeth.transform.position.y+6f);
		if (time<0.5f) {
			transform.GetChild (0).GetComponent<Text> ().color = v;
		//	transform.GetChild (0).GetComponent<Text> ().color=Color.Lerp(Color.black,v,0.5f*Time.deltaTime);	
		}
		if (time > 0) {
			time -= Time.deltaTime;
		}
	}
}
