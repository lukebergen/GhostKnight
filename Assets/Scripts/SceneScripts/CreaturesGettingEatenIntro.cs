using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreaturesGettingEatenIntro : MonoBehaviour {

	public GameObject Player;
	public GameObject EatenGuy;
	public GameObject GetAwayGuy;
	public GameObject FallThroughPlatformTrigger1;
	public GameObject FallThroughPlatformTrigger2;
	public GameObject Cam;

	private bool sceneStarted = false;

	void FixedUpdate() {
		if (!sceneStarted && Vector2.Distance (Player.transform.position, GetAwayGuy.transform.position) < 1.0f) {
			sceneStarted = true;
			StartCoroutine(RunScene ());
		}
	}

	IEnumerator RunScene() {
		CharacterController gagController = GetAwayGuy.GetComponent<CharacterController> ();
		CharacterController egController = EatenGuy.GetComponent<CharacterController> ();
		OneWayPlatformTrigger platform1 = FallThroughPlatformTrigger1.GetComponent<OneWayPlatformTrigger>();
		OneWayPlatformTrigger platform2 = FallThroughPlatformTrigger2.GetComponent<OneWayPlatformTrigger>();

		// Steal camera, disable player input
		Cam.GetComponent<CameraController> ().Target = GetAwayGuy;
		Player.GetComponent<ProtagController> ().inputEnabled = false;

		gagController.Walk ("right");
		egController.Walk ("right");
		yield return new WaitForSeconds(5.0f); // Just walkin' along like nothing's up

		Debug.Log("Have eaten guy be eaten (trigger animations and such) and show '!' over get away guy");
		egController.Idle();
		gagController.Idle();
		yield return new WaitForSeconds(1.0f); // freaking out time

		gagController.Run ("left");
		platform1.manuallyPassThrough = true;  // simulate the creature "pressing down" to fall through by manually disabling the platform
		yield return new WaitForSeconds(1.3f); // running back time

		gagController.Run ("right");

		yield return new WaitForSeconds(2.3f); // down the first ramp going right
		platform1.manuallyPassThrough = false; // cleaning up after ourselves

		gagController.Run ("left");
		platform2.manuallyPassThrough = true;

		yield return new WaitForSeconds(1.75f); // down the second ramp going left

		gagController.Run("right");

		yield return new WaitForSeconds(4.75f); // running for the tree
		platform2.manuallyPassThrough = false;

		gagController.Idle ();

		yield return new WaitForSeconds(0.5f); // catches his breath for a second

		// and, relealizing that the danger is past, he meanders around the tree
		Dictionary<string, object> arguments = new Dictionary<string, object>();
		arguments.Add ("period", 2.0f);
		arguments.Add ("subject", GetAwayGuy);
		GetAwayGuy.GetComponent<MovementAnimations>().SetAnimation("WalkBackAndForth", arguments);

		yield return new WaitForSeconds (5.0f); // show him walking around tree for a couple seconds

		// return control to the player
		Cam.GetComponent<CameraController> ().Target = Player;
		Player.GetComponent<ProtagController> ().inputEnabled = true;
	}
}
