using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSaveData {
	public static int activearea=0;
	public static float[] EmethPosition={9.6f,-2.3f};
	public static float Emethscalex=2.261674f;
	public static string[] items={}; 
	public static string[] deleteditems={};
	public static int[] plugs={1,1,1,1};
	public static bool[] bools={false,true,false,false,true,false,false,false,false};
	public static void GiveFromGameSave(GameSaveClass C){
		activearea = C.activearea;
		EmethPosition=C.EmethPosition;
		Emethscalex=C.Emethscalex;
		items=C.items; 
		deleteditems=C.deleteditems;
		plugs = (int[])C.plugs.Clone ();
		bools = (bool[])C.bools.Clone();
	}
}
