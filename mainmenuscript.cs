using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class mainmenuscript : MonoBehaviour {
	private Button newgame;
	private Button loadgame;
	private Button exit;
	public GameObject exitrusure;
	public GameObject LoadGamePanel;
	// Use this for initialization
	void Start () {
		exitrusure.SetActive (false);
		LoadGamePanel.SetActive (false);
		newgame = transform.GetChild (0).GetComponent<Button> ();
		newgame.GetComponent<Button>().onClick.AddListener (NewGame);
		loadgame = transform.GetChild (1).GetComponent<Button> ();
		exit = transform.GetChild (2).GetComponent<Button> ();
		loadgame.onClick.AddListener (LoadGame);
		exit.onClick.AddListener (Exit);

		exitrusure.transform.GetChild (0).GetComponent<Button> ().onClick.AddListener(Application.Quit);
		exitrusure.transform.GetChild (1).GetComponent<Button> ().onClick.AddListener(exitexit);
	}

	public void NewGame(){
		GameSaveClass temp = new GameSaveClass ();
		GameSaveData.GiveFromGameSave (temp);
		SceneManager.LoadScene ("scene1");
	}
	void LoadGame(){
		LoadGamePanel.SetActive (true);
	}
	void Exit(){
		exitrusure.SetActive (true);
	}
	void exitexit(){
		exitrusure.SetActive (false);
	}
}
