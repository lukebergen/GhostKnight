using UnityEngine;
using System.Collections;

public class SurfStoneController : MonoBehaviour {

	public float FloatLevel;

	private SpringJoint2D spring;
	// Use this for initialization
	void Start () {
		spring = gameObject.GetComponent<SpringJoint2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (nearGround()) {
			if (!spring.enabled) {
				spring.enabled = true;
			}

			spring.connectedAnchor.Set(transform.position.x, groundDistance() - 0.4f);
		}
	}

	public bool nearGround() {
		return groundDistance() < FloatLevel;
	}

	public float groundDistance() {
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -Vector2.up, this.FloatLevel, LayerMask.NameToLayer("Ground"));
		Debug.Log("ground distance: " + hitInfo.distance);
		return 0.05f;
	}
}
