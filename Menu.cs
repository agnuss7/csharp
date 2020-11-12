using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {
	public GameObject controller;
	public GameObject emeth;
	public GameObject Inventory;
	public GameObject Table;
	public GameObject rusure;
	public GameObject loadingpanel;
	public GameObject Loadingtext;
	// Use this for initialization
	void Start () {
		transform.GetChild (0).GetChild (0).GetComponent<Button> ().onClick.AddListener (Back);
		transform.GetChild (0).GetChild (1).GetComponent<Button> ().onClick.AddListener (Save);
		transform.GetChild (0).GetChild (2).GetComponent<Button> ().onClick.AddListener (Load);
		transform.GetChild (0).GetChild (3).GetComponent<Button> ().onClick.AddListener (tommenu);
		transform.GetChild (0).GetChild (4).GetComponent<Button> ().onClick.AddListener (quit);

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Back ();
		}
	}


	void Save(){
		
		SaveTheGame.Save(controller.GetComponent<ControllerScript> (), emeth.GetComponent<EmethScript> (), Inventory,Table);

		Loadingtext.SetActive (true);
	}
	void Back(){
		Loadingtext.SetActive (false);
		controller.SetActive (true);
		Inventory.SetActive (true);
		loadingpanel.SetActive (false);
		gameObject.SetActive (false);

	}
	void Load(){
		loadingpanel.SetActive(true);
	}

	void quit(){
		rusure.GetComponent<RusureScript> ().yesFun = quittodesk;
		rusure.GetComponent<RusureScript> ().warning="Are you sure you want to quit to desktop? All unsaved progress will be lost.";
		rusure.SetActive (true);
	}
	void tommenu(){
		rusure.GetComponent<RusureScript> ().yesFun = backtommenu;
		rusure.GetComponent<RusureScript> ().warning="Are you sure you want to go back to the main menu? All unsaved progress will be lost.";
		rusure.SetActive (true);
	}

	void quittodesk(){
		Application.Quit ();
	}
	void backtommenu(){
		SceneManager.LoadScene ("scene2");
	}
}
