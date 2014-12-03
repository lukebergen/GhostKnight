using UnityEngine;
using System.Collections;

public class BalastController : MonoBehaviour {

	private GameObject player;
	private bool pickupable = false;

	public void Start() {
		player = GameObject.Find ("Player");
	}

	public void CutInteraction() {
		if (pickupable) {
			Debug.Log ("Picking up balast thing");
			player.GetComponent<ProtagAttackController>().pickUp(gameObject);
		} else {
			rigidbody2D.isKinematic = false;
			pickupable = true;
		}
	}
}
