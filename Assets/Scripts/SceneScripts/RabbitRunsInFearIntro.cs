using UnityEngine;
using System.Collections;

public class RabbitRunsInFearIntro : MonoBehaviour {	
	public GameObject Player;
	public GameObject ScaredRabbit;
	public GameObject Cam;

	public float XAtWhichToStart;
	
	private bool sceneStarted = false;
	
	void FixedUpdate() {
		if (!sceneStarted && Player.transform.position.x >= XAtWhichToStart) {
			sceneStarted = true;
			StartCoroutine(RunScene ());
		}
	}
	
	IEnumerator RunScene() {
		CharacterController rabbitController = ScaredRabbit.GetComponent<CharacterController> ();
		
		Cam.GetComponent<CameraController> ().Target = ScaredRabbit;
		Player.GetComponent<ProtagController> ().inputEnabled = false;

		Debug.Log("! of fear over rabbit head animation");
		yield return new WaitForSeconds(1.0f); // freaking out time

		rabbitController.Run ("left");
		yield return new WaitForSeconds(1.0f); // running from player time

		rabbitController.Idle ();

		yield return new WaitForSeconds(1.0f); // watch him chillin' out

		Player.GetComponent<ProtagController> ().inputEnabled = true;
		Cam.GetComponent<CameraController> ().Target = Player;
	}
}
