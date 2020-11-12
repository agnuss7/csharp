using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyondisable : MonoBehaviour {

	void OnDisable () {
		Destroy(gameObject);
	}
}
