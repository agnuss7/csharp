using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PickUpItemScript : MonoBehaviour, IPointerClickHandler {

	public GameObject mouse;
	public GameObject CommentController;
	public string comment;
	// Use this for initialization
	void Start () {
		//GetComponent<Button> ().onClick.AddListener (Clicky);
		if (mouse == null) {
			mouse = GameObject.Find ("MouseText").gameObject;
		}
		if (CommentController == null) {
			CommentController = GameObject.Find ("CommentSpace").gameObject;
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			transform.parent.parent.SendMessage ("clickedItem",name);
			mouse.SendMessage ("SetImage",GetComponent<Image>().sprite);
		} else if (eventData.button == PointerEventData.InputButton.Right) {
			CommentController.SendMessage ("StartComment", comment);
			transform.parent.parent.SendMessage ("RightClickFunctionality",name);
		}
	}
	/*void Clicky () {
		transform.parent.parent.SendMessage ("clickedItem",name);
		mouse.SendMessage ("SetImage",GetComponent<Image>().sprite);
	}*/


}
