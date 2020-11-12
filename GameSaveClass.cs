using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public  class GameSaveClass {

	public  int activearea;
	public  float[] EmethPosition;
	public  float Emethscalex;
	public  string[] items; 
	public  string[] deleteditems;
	public bool[] bools;
	public int[] plugs;
	public GameSaveClass(){
		activearea=0;
		EmethPosition=new float[]{9.6f,-2.3f};
		Emethscalex=2.261674f;
		items=new string[]{}; 
		deleteditems=new string[]{};
		plugs=new int[]{1,1,1,1};
		bools=new bool[]{false,true,false,false,true,false,false,false,false};
	}
	public GameSaveClass(ControllerScript controller, EmethScript e,GameObject I,GameObject Table){

		activearea=controller.activeArea.currentAreaIndex();
		deleteditems = controller.deleteditems.ToArray();
		EmethPosition = new float[]{e.transform.position.x,e.transform.position.y ,e.transform.position.z};
		Emethscalex = e.transform.localScale.x;
		items=new string[I.transform.GetChild (0).childCount];
		for (int i = 0; i < I.transform.GetChild (0).childCount; i++) {
			items[i] = I.transform.GetChild (0).GetChild (i).name;
		}
		bools = (bool[])BoolsInts.boolies.Clone();
		plugs = (int[])Table.GetComponent<TableCloseUpScript> ().plugModes.Clone ();
	}
}
