using UnityEngine;
using System.Collections;
using System;

public class ProtagAttackController : MonoBehaviour {

	public GameObject item;

	private bool doAttack = false;
	private bool doAttack2 = false;

	// Use this for initialization
	void Start () {
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			doAttack = true;
		}
		if (Input.GetButtonDown ("Fire2")) {
			doAttack2 = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (doAttack) {
			doAttack = false;
			int direction = gameObject.GetComponent<ProtagController>().direction;
			Vector2 p1 = new Vector2(transform.position.x + (direction * 0.15f), transform.position.y + 0.1f);
			Vector2 p2 = new Vector2(transform.position.x + (direction * 0.15f), transform.position.y - 0.1f);
			Debug.DrawLine(new Vector3(p1.x, p1.y, -0.5f), new Vector3(p2.x, p2.y, -0.5f), Color.blue, 10.0f);
			Collider2D[] collisions = Physics2D.OverlapAreaAll(p1, p2);
			for (int i = 0 ; i < collisions.Length ; i++) {
				InteractionsController other = collisions[i].gameObject.GetComponent<InteractionsController>();
				if (other != null && other.hasType(InteractionsController.InteractionType.Cuttable)) {
					other.gameObject.SendMessage("CutInteraction");
				}
			}
		}

		if (doAttack2) {

		}
	}

	public void pickUp(GameObject item) {
		this.item = item;
	}
}
