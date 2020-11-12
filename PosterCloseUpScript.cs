using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PosterCloseUpScript : MonoBehaviour {
	public GameObject Controller;
	public GameObject Inventory;
	private Button close;
	void Start () {
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (Clicky);

	}
	void OnEnable(){
		Controller.SetActive (false);
		Inventory.SetActive (false);
	}
	void Clicky(){
		Inventory.SetActive (true);
		Controller.SetActive (true);
		gameObject.SetActive (false);
	}
}
