using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Target;

	// Update is called once per frame
	void Update () {
		Vector3 pos = Target.transform.position;
		transform.position = new Vector3 (pos.x, pos.y + 0.4f, transform.position.z);
	}
}
