using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Text;

public static class SaveTheGame {
	public static void Save(ControllerScript controller, EmethScript e,GameObject I,GameObject Table){
		BinaryFormatter form = new BinaryFormatter ();
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
		string name = System.DateTime.Now.Month.ToString () + System.DateTime.Now.Day.ToString () + System.DateTime.Now.Hour.ToString () + System.DateTime.Now.Minute.ToString ()+ System.DateTime.Now.Second.ToString ();
		string path = Application.persistentDataPath + "/SavedGames/"+name+".gm";
		FileStream stream = new FileStream (path, FileMode.Create);

		GameSaveClass c=new GameSaveClass (controller, e, I,Table);
		form.Serialize (stream, c);
		stream.Close ();
		string xml="<save><image>"+name+".jpg</image><timestamp>"+System.DateTime.Now.ToString()+"</timestamp><file>"+name+".gm</file></save>\n</xml>";

		using(var fs = new FileStream(savelog,FileMode.Open))
		{
			fs.Seek(-6,SeekOrigin.End);
			var newText = Encoding.UTF8.GetBytes(xml);
			fs.Write(newText,0,newText.Length);
			fs.Close ();
		}
		/*
		string old = File.ReadAllText (savelog);
		old=old.Remove(old.Length - 6) + xml;
		File.WriteAllText(savelog, old);
		*/
		screenshotscript.TakeScreenshot_stat (name);
	}
	public static void LoadTheGame(string path){
		if (File.Exists (path)) {
			BinaryFormatter form = new BinaryFormatter ();
			FileStream stream = new FileStream (path, FileMode.Open);
			GameSaveClass c= form.Deserialize (stream) as GameSaveClass;
			stream.Close ();
			GameSaveData.GiveFromGameSave (c);
			SceneManager.LoadScene ("scene1");
		} else {
			path=Application.persistentDataPath + "/SavedGames/try.gm";
			BinaryFormatter form = new BinaryFormatter ();
			FileStream stream = new FileStream (path, FileMode.Open);
			GameSaveClass c= form.Deserialize (stream) as GameSaveClass;
			stream.Close ();
			GameSaveData.GiveFromGameSave (c);
			SceneManager.LoadScene ("scene1");
		}
	}
		
}
