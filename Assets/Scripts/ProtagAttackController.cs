using UnityEngine;
using System.Collections;
using System;

public class ProtagAttackController : MonoBehaviour {

	public GameObject item;

	private bool doAttack = false;
	private bool doAttack2 = false;
	private bool doCrouchAttack = false;

	// Use this for initialization
	void Start () {
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (Input.GetAxis("Vertical") < 0) {
				doCrouchAttack = true;
			} else {
				doAttack = true;
			}
		}
		if (Input.GetButtonDown ("Fire2")) {
			doAttack2 = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (doAttack || doCrouchAttack) {
			int direction = gameObject.GetComponent<ProtagController> ().direction;
			Vector2 p1 = new Vector2 (transform.position.x + (direction * 0.2f) + 0.05f, transform.position.y + 0.1f);
			Vector2 p2 = new Vector2 (transform.position.x + (direction * 0.2f) - 0.05f, transform.position.y - 0.1f);

			Debug.DrawLine(new Vector3(p1.x, p1.y, 1.0f), new Vector3(p2.x, p2.y, 1.0f), Color.red, 1.0f);

			Collider2D[] collisions = Physics2D.OverlapAreaAll (p1, p2);

			for (int i = 0 ; i < collisions.Length ; i++) {
				if (doAttack) {
					Debug.Log ("cutting: " + collisions[i].name);
					collisions[i].gameObject.SendMessage("OnCut", gameObject, SendMessageOptions.DontRequireReceiver);
				}
				if (doCrouchAttack) {
					collisions[i].gameObject.SendMessage("OnCrouchAttack", gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}
			doAttack = false;
			doCrouchAttack = false;
		}

		if (doAttack2) {
			doAttack2 = false;
			if (this.item != null) {
				this.item.SendMessage("fire", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public void pickUp(GameObject item) {
		this.item = item;
		this.item.SendMessage ("OnPickedUp", this.gameObject, SendMessageOptions.DontRequireReceiver);
	}
}
