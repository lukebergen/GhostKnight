using UnityEngine;
using System.Collections;

public class SpaceHider : MonoBehaviour {

	public float OpacityOnHide = 0.0f;
	private GameObject player;

	void Start() {
		player = GameObject.Find("Player");
	}

	void OnTriggerEnter2D(Collider2D other) {
		// I'm unsure if this should show/hide whenever /anything/ enters it
		// or if it should be exlusive to when the player enters/exits.
		// Hopefully usage will clarify. Should be fairly easy to do it either way
		if (other.gameObject == player || true) {
			Show ();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject == player || true) {
			Hide ();
		}
	}

	public void Show() {
		this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, OpacityOnHide);
	}

	public void Hide() {
		this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
}
