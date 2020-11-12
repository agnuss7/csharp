using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Text;
using System;
using UnityEngine.UI;

[System.Serializable]
public class LoadPanel{
	public Sprite img;
	public string name;
	public string time;
	public LoadPanel(string picturename, string file, string timestamp){
		byte[] decode = File.ReadAllBytes(Application.persistentDataPath+"/SavedGames/"+picturename);
		Texture2D tex = new Texture2D (2, 2);
		tex.LoadImage (decode);
		img = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height),new Vector2(0,0));
		name = file;
		time = timestamp;
	}
}

public class LoadGamePanelScript : MonoBehaviour {
	public GameObject rusure;
	public GameObject content;
	public GameObject screen;
	public GameObject prefab;
	private string file;
	public GameObject Exitbutt;
	public GameObject Loadbutt;
	void Start(){
		Exitbutt.GetComponent<Button> ().onClick.AddListener (Exit);
		Loadbutt.GetComponent<Button> ().onClick.AddListener (PressLoad);
	}
	void OnEnable () {
		string folder = Application.persistentDataPath + "/SavedGames";
		string savelog=Application.persistentDataPath + "/SavedGames/savelog.xml";
		if (!Directory.Exists (folder)) {
			Directory.CreateDirectory (folder);
		}
		if (!File.Exists (savelog)) {
			//File.Create (savelog);
			using(var fs = new FileStream(savelog,FileMode.OpenOrCreate,FileAccess.Write,FileShare.ReadWrite))
			{
				var newText = Encoding.UTF8.GetBytes("<xml>\n</xml>");
				fs.Write(newText,0,newText.Length);
				fs.Close ();
			}
		}
		XmlDocument doc = new XmlDocument();
		//doc.LoadXml ("<xml><save><name>first</name><timestamp></timestamp><file>"+name+".gm</file></save><save><name>first</name><timestamp>"+System.DateTime.Now.ToString()+"</timestamp><file>"+name+"2.gm</file></save></xml>");
		doc.Load(savelog);
		List<LoadPanel> Loads=new List<LoadPanel>();
		foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
			LoadPanel New = new LoadPanel (node.ChildNodes[0].InnerText,node.ChildNodes[2].InnerText,node.ChildNodes[1].InnerText);
			Loads.Add (New);
		}
		//screen.GetComponent<Image> ().sprite = Loads [0].img;

		int temp=Loads.Count-1;
		for(int i=temp;i>=0;i--){
			
			GameObject temporary=GameObject.Instantiate (prefab, content.transform);
			temporary.transform.GetChild (0).GetComponent<Text> ().text = Loads [i].time;
			string nn = Loads [i].name;
			Sprite spr = Loads [i].img;
			temporary.GetComponent<Button>().onClick.AddListener(()=>ChooseLoad(nn,spr));
		}

	}

	void ChooseLoad(string filename,Sprite img){
		screen.GetComponent<Image> ().sprite = img;
		file = filename;
	} 
	void PressLoad(){
		if (rusure == null) {
			Load ();
		} else {
			rusure.GetComponent<RusureScript> ().yesFun = Load;
			rusure.GetComponent<RusureScript> ().warning="Are you sure you want to load another save? All unsaved progress will be lost.";
			rusure.SetActive (true);
		}
	}
	void Load(){
		if (file != null && file != "") {
			SaveTheGame.LoadTheGame (Application.persistentDataPath+"/SavedGames/"+file);
		}
	}
	void Exit(){
		gameObject.SetActive (false);
	}
}
