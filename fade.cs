using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour {
	private float seconds;
	void OnEnable () {
		seconds = 2f;

	}
	void Update(){
		seconds -= Time.deltaTime;
		if (seconds < 0) {
			gameObject.SetActive (false);
		}
	}

}
