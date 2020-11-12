using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManScript : MonoBehaviour {
	public GameObject Dialogue;
	public GameObject CommentControl;
	public GameObject Inventory;
	private int firsttime=4;
	private int detriangled = 5;
	public PickUpItem p;
	public void Operate (string op) {
		if (op == "") {
			if (BoolsInts.boolies[firsttime]) {
				Dialogue.SetActive (true);
				dial d = new dial (new Sentence ("Excuse me?", 1, 0, "Emeth:"), 0, 1);
				d.passSentence (new Sentence ("Hi there, kid!", 3, 1, "Old Man:"));
				d.passSentence (new Sentence ("Uuh... Do you know where we are?", 1, 1, "Emeth:"));
				d.passSentence (new Sentence ("(huh?.. that shape on his forehead...)", 2, 1, "Emeth:"));
				d.passSentence (new Sentence ("I'm sleepy...", 3, 2, "Old Man:"));
				d.passSentence (new Sentence ("zzzzzzzzzzz", 3, 3, "Old Man:"));
				d.passSentence (new Sentence ("Guess he knows nothing.", 1, 3, "Emeth:"));
				Dialogue.SendMessage ("StartDialogue", d);
				GetComponent<CommentScript> ().comment = "The old man's sound asleep.";
				BoolsInts.boolies[firsttime] = false;
			} else if (!BoolsInts.boolies[detriangled]) {
				Dialogue.SetActive (true);
				dial d = new dial (new Sentence ("zzzzzzz", 3, 3, "Old man:"), 0, 1);
				d.passSentence (new Sentence ("I won't disturb him.", 1, 3, "Emeth:"));
				Dialogue.SendMessage ("StartDialogue", d);

			} else {
				Dialogue.SetActive (true);
				dial d = new dial (new Sentence ("zzzzzzz", 3, 4, "Old man:"), 0, 1);
				d.passSentence (new Sentence ("I won't disturb him.", 1, 4, "Emeth:"));
				Dialogue.SendMessage ("StartDialogue", d);
			}
		} else if (op == "scalpel" && BoolsInts.boolies[firsttime]==false && BoolsInts.boolies[detriangled]==false) {
			Dialogue.SetActive (true);
			dial d = new dial (new Sentence ("(I hope he doesn't wake up...)", 2, 3, "Emeth:"), 0, 1);
			d.passSentence (new Sentence ("I knew it. There was a shape sewn under his skin.", 1, 4, "Emeth:"));
			d.passSentence (new Sentence ("This was horribly unsanitary, but all in all, I did him a favour.", 3, 4, "Emeth:"));
			Dialogue.SendMessage ("StartDialogue", d);
			Inventory.SendMessage ("AddItem",p.name);
			BoolsInts.boolies[detriangled] = true;
		} else {
			CommentControl.SendMessage ("InvalidComment");
		}
	}
	

}
