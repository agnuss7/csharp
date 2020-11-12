using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class screenshotscript : MonoBehaviour {
	private Camera cam;
	private static screenshotscript instance;
	private static string name;
	private bool wasshot;
	void Awake () {
		instance = this;
		cam = gameObject.GetComponent<Camera> ();
	}
	void Update(){
		//transform.position = Camera.main.transform.position;
	}
	private void OnPostRender(){
		if (wasshot) {
			wasshot = false;
			RenderTexture renderTexture = cam.targetTexture;
			Texture2D renderResult = new Texture2D (renderTexture.width, renderTexture.height);
			Rect rect = new Rect (0,0,renderTexture.width, renderTexture.height);
			renderResult.ReadPixels (rect, 0, 0);
			byte[] arr = renderResult.EncodeToJPG(10);
			string folder=Application.persistentDataPath+"/SavedGames";
			if (!Directory.Exists (folder)) {
				Directory.CreateDirectory (folder);
			}
			System.IO.File.WriteAllBytes (folder+"/"+name+".jpg",arr);
			RenderTexture.ReleaseTemporary (renderTexture);
			cam.targetTexture = null;
			/*string xml="<save><image></image><timestamp>"+System.DateTime.Now.ToString()+"</timestamp><file>"+name+"</file></save>\n</xml>";
			using(var fs = new FileStream(Application.persistentDataPath+"/SavedGames/savelog.xml",FileMode.Open))
			{
				fs.Seek(-6,SeekOrigin.End);
				var newText = Encoding.UTF8.GetBytes(xml);
				fs.Write(newText,0,newText.Length);
				fs.Close ();
			}
			cam.targetTexture = null;
			Debug.Log (base64);
			*/
		}

	}
	private void TakeScreenshot(){
		cam.targetTexture = RenderTexture.GetTemporary (Screen.width, Screen.height);
		wasshot = true;
	}

	public static void TakeScreenshot_stat(string n){
		name=n;
		instance.TakeScreenshot ();
	}
}
