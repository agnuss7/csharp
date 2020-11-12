using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour{
	public GameObject Controller;
	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			if (transform.GetComponent<Collider2D> ().OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition))) {

				if (Controller.activeSelf) {
					Controller.SetActive (false);
				} else {
					Controller.SetActive (true);
				}
			}
		}
	}




}