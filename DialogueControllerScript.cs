using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sentence{
	public string sentence;
	public int mood1;
	public int mood2;
	public string name;
	public Sentence(string s,int m1, int m2, string n){
		sentence = s;
		mood1 = m1;
		mood2 = m2;
		name = n;
	}
}
	
[System.Serializable]
public class dial{
	public dial(Sentence ss,int c, int? d){
		sss = new Queue<Sentence> ();
		sss.Enqueue (ss);
		a = c;
		b = d;
	}
	public dial(Queue<Sentence> sen,int c, int? d){
		sss = sen;
		a = c;
		b = d;
	}
	public void passSentence(Sentence sen){
		sss.Enqueue (sen);
	}
	public Queue<Sentence> sss;
	public int a;
	public int? b;
}


public class DialogueControllerScript : MonoBehaviour {
	public RuntimeAnimatorController[] anims;
	public GameObject Controller;
	private Queue<Sentence> Lines=new Queue<Sentence>();


	void Start(){
		float height = 2f * Camera.main.orthographicSize;
		float width=height*Camera.main.aspect;
		//transform.GetChild (0).transform.localScale=new Vector3(width*transform.lossyScale.x,height);
	}

	void StartDialogue(dial dd){
		Controller.SetActive (false);
		transform.GetChild(1).GetComponent<Animator> ().runtimeAnimatorController = anims[dd.a];
		if (dd.b != null) {
			transform.GetChild (2).GetComponent<Animator> ().runtimeAnimatorController = anims [(int)dd.b];
		} else {
		transform.GetChild (2).GetComponent<Animator> ().runtimeAnimatorController = null;
		}
		Lines = dd.sss;
		NextLine ();
	}
	void NextLine(){
		Sentence s = Lines.Dequeue ();
		transform.GetChild (3).GetChild (0).GetComponent<Text> ().text=s.sentence;
		transform.GetChild (3).GetChild (1).GetComponent<Text> ().text = s.name;
		transform.GetChild (1).GetComponent<Animator> ().SetInteger ("Mood",s.mood1);
		if (transform.GetChild (2).GetComponent<Animator> ().runtimeAnimatorController != null) {
			transform.GetChild (2).GetComponent<Animator> ().SetInteger ("Mood",s.mood2);
		}
	}
	void EndDialogue(){
		Controller.SetActive (true);
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
				if (Lines.Count <= 0) {
					EndDialogue ();
				} else {
					NextLine ();
				}


		}
	}
}
