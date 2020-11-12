using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : MonoBehaviour {
	public GameObject Emeth;
	public GameObject Controller;
	public GameObject Inventory;
	public GameObject Table;
	public GameObject passage;
	void Awake () {
		Emeth.transform.position = new Vector2 (GameSaveData.EmethPosition[0],GameSaveData.EmethPosition[1]);
		Emeth.transform.localScale = new Vector3 (GameSaveData.Emethscalex,Emeth.transform.localScale.y);
		Controller.GetComponent<ControllerScript> ().OnLoadActiveArea = GameSaveData.activearea;
		Controller.GetComponent<ControllerScript> ().deleteditems.AddRange (GameSaveData.deleteditems);
		foreach (string i in GameSaveData.items) {
			Inventory.SendMessage ("AddItem",i);
		}
		BoolsInts.boolies = (bool[])GameSaveData.bools.Clone();
		//Debug.Log (GameSaveData.plugs[0].ToString()+" "+GameSaveData.plugs[1].ToString()+" "+GameSaveData.plugs[2].ToString()+" "+GameSaveData.plugs[3].ToString());
		Table.GetComponent<TableCloseUpScript> ().plugModes = (int[])GameSaveData.plugs.Clone();

	}
	

}
