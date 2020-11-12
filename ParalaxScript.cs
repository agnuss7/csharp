using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScript : MonoBehaviour {

	void Operate(){

	}

	public GameObject Emeth;
	void Start () {
		Emeth = GameObject.Find ("Emeth").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x,-2f*(Camera.main.transform.position.y)-5.7f);
	}
}
