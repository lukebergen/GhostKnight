using UnityEngine;
using System.Collections;

public class BalastController : MonoBehaviour {

	private bool pickupable = false;

	public void Start() {
	}

	public void OnCut(GameObject cutter) {
		if (!pickupable) {
			rigidbody2D.isKinematic = false;
			pickupable = true;
		}
	}

	public void OnPet(GameObject petter) {
		Debug.Log ("Petting");
	}

	// player.GetComponent<ProtagAttackController>().pickUp(gameObject);
}
