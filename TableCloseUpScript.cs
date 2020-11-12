using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TableCloseUpScript : MonoBehaviour {
	public GameObject passage;
	private int done = 7;
	public Sprite _plug;
	public Sprite[] setvials;
	public GameObject Controller;
	public GameObject Inventory;
	private int cnt = 4;
	private string operate;
	public GameObject[] plugs;

	public int[] plugModes;
	private string[] vialnames;
	// Use this for initialization
	void Start () {
		operate = "";
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (click);

		for (int i = 0; i < cnt; i++) {
			if (plugModes [i] == 0) {
				plugs [i].GetComponent<SpriteRenderer> ().sprite = _plug;
			} 
			for (int j = 1; j < 6; j++) {
				if (plugModes [i] == j) {
					plugs[i].GetComponent<SpriteRenderer>().sprite=setvials[j-1];
					break;
				}
			}
		}
		vialnames=new string[]{"empty vial","yellow vial","black vial","purple vial","blood vial"};
	}
		


	// Update is called once per frame
	void Update () {
		Controller.SetActive (false);
		operate=Inventory.GetComponent<InventoryScript> ().combining;
		if (Input.GetMouseButtonDown (0)) {
			Vector2 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			bool got = false;
			for (int i = 0; i < cnt; i++) {
				if (plugs [i].GetComponent<Collider2D> ().OverlapPoint (position)) {
					if (operate == "") {
						for (int j = 1; j < 6; j++) {
							if (j == plugModes [i]) {
								plugs [i].GetComponent<SpriteRenderer> ().sprite = _plug;
								Inventory.SendMessage ("AddItem",vialnames[j-1]);
								//Inventory.SendMessage ("AddItem",new PickUpItem(returnvials[j-1],vialnames[j-1],vialcomments[j-1]));
								plugModes [i] = 0;
								break;
							}
						}
					} else {
						for (int o = 1; o < 6; o++) {
							if (vialnames [o-1] == operate) {
								for (int j = 1; j < 6; j++) {
									if (j == plugModes [i]) {
										//Inventory.SendMessage ("AddItem",new PickUpItem(returnvials[j-1],vialnames[j-1],vialcomments[j-1]));
										Inventory.SendMessage ("AddItem",vialnames[j-1]);
										break;
									}
								}
								//
								plugs[i].GetComponent<SpriteRenderer>().sprite=setvials[o-1];
								plugModes [i] = o;
								Inventory.SendMessage ("DeleteItem",vialnames[o-1]);
								//
								break;
							}
						}
						Inventory.SendMessage ("CombItemNull");

					}
					break;
				}
			}
		}
		if (plugModes [0] == 3 && plugModes [1] == 2 && plugModes [2] == 5 && plugModes [3] == 4) {
			BoolsInts.boolies[done] = true;
			passage.SendMessage ("OpenUp");
			Controller.SetActive (true);
			gameObject.SetActive (false);
		}
	}
	void click(){
		Controller.SetActive (true);
		gameObject.SetActive (false);
	}
}
