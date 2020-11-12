using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class passageScript : MonoBehaviour {

	void Operate(){
		if (BoolsInts.boolies [6]) {

			SceneManager.LoadScene ("scene2");

		}
	}

}
