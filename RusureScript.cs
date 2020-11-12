using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RusureScript : MonoBehaviour {
	public delegate void yesFunction();
	public yesFunction yesFun;
	public string warning;
	// Use this for initialization
	void Start(){
		transform.GetChild (0).GetChild (1).GetComponent<Button> ().onClick.AddListener (yes);
		transform.GetChild (0).GetChild (2).GetComponent<Button> ().onClick.AddListener (no);
	}
	void OnEnable () {
		transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = warning;
	}
	
	void no(){
		gameObject.SetActive (false);
	}
	void yes(){
		yesFun ();
	}
}
