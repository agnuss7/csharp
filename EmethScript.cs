using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmethScript : MonoBehaviour {
	public GameObject Comment;
	public float speed = 3f;
	public Animator animator;
	//private bool moving=false;
	private Stack<Vector2> coordinates=new Stack<Vector2>();
	void Start(){
		if (Comment == null) {
			Comment = GameObject.Find ("CommentSpace").gameObject;
		}
	}
	public void GiveCoordinates(Stack<Vector2> coo){
		coordinates = coo;
	}
	private void WalkTo() {
		if (coordinates.Peek ().x < transform.position.x) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
			}
		} else {
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
			}
		}
		if (coordinates.Peek ().x - transform.position.x == 0f) {
			if (coordinates.Peek ().y < transform.position.y) {
				animator.SetBool ("MovingDown", true);
				animator.SetBool ("MovingSide", false);
				animator.SetBool ("MovingUp", false);
			} else {
				animator.SetBool ("MovingDown",false);
				animator.SetBool ("MovingSide",false);
				animator.SetBool ("MovingUp",true);
			}
		} else {
			animator.SetBool ("MovingDown",false);
			animator.SetBool ("MovingSide",true);
			animator.SetBool ("MovingUp",false);
		}

		transform.position = Vector2.MoveTowards (transform.position, coordinates.Peek(), speed * Time.deltaTime);
		if ((Vector2)transform.position == coordinates.Peek()) {
			coordinates.Pop();
		}

	}
	void Operate (string op) {
		if (op == "syringe") {
			Comment.SendMessage ("StartComment", "Ouch!");
			GameObject.Find ("Inventory").SendMessage ("AddItem","blood syringe");
			GameObject.Find ("Inventory").SendMessage ("DeleteItem","syringe");
		} else if (op!=""){
			Comment.SendMessage ("InvalidComment");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (coordinates.Count > 0) {
			WalkTo ();
			//animator.SetBool ("Moving",true);
		} else {
			animator.SetBool ("MovingDown",false);
			animator.SetBool ("MovingSide",false);
			animator.SetBool ("MovingUp",false);
		}

	}
}
