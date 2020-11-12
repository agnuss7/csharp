using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseScript : MonoBehaviour {
	public GameObject Controller;
	public GameObject Inventory;
	private Color v=Color.white;
	private Color v2=Color.white;
	// Use this for initialization
	void Awake () {
		v.a = 0;
		transform.GetChild (0).GetComponent<Text> ().color = v;
	}
	// Update is called once per frame
	void Update () {
		Vector3 vv = Input.mousePosition;
		vv.z = 10f;
		transform.position = Camera.main.ScreenToWorldPoint(vv);


	}
	void SetImage(Sprite a){
		transform.GetChild (1).GetComponent<Image> ().sprite = a;
	}
	void VisibleImage(){
		transform.GetChild (1).GetComponent<Image> ().color = v2;
	}
	void NoImage(){
		transform.GetChild (1).GetComponent<Image> ().color = v;
	}
}
