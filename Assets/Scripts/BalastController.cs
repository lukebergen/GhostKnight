using UnityEngine;
using System.Collections;

public class BalastController : MonoBehaviour {
	
	private GameObject player;

	private bool pickupable = false;

	public void Start() {
	}

	public void OnCut(GameObject cutter) {
		if (!pickupable) {
			rigidbody2D.isKinematic = false;
			pickupable = true;
		}
	}

	public void OnCrouchAttack(GameObject sender) {
		// I am pickupable so let the sender know that doing this means picking me up.
		sender.SendMessage ("pickUp", this.gameObject, SendMessageOptions.DontRequireReceiver);
	}

	public void OnPickedUp(GameObject player) {
		this.player = player;
		this.renderer.enabled = false;
		Debug.Log ("Balast object: Picked up by player");
	}

	public void fire() {
		Debug.Log ("Doing something with balast controller.");
	}
}
