using UnityEngine;
using System.Collections;

public class OneWayPlatformTrigger : MonoBehaviour {
	
	public bool passThrough = false;
	public bool manuallyPassThrough = false;

	private Collider2D platform;
	private Rigidbody2D playerBody;
	
	// Use this for initialization
	void Awake () {
		platform = transform.parent.gameObject.GetComponent<EdgeCollider2D>();
		if (platform == null) { platform = transform.parent.gameObject.GetComponent<BoxCollider2D>(); }
		if (platform == null) { platform = transform.parent.gameObject.GetComponent<PolygonCollider2D>(); }
		if (platform == null) { platform = transform.parent.gameObject.GetComponent<CircleCollider2D>(); }

		playerBody = GameObject.Find ("Player").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		platform.enabled = !passThrough && playerBody.velocity.y < 0.0f && !manuallyPassThrough;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		passThrough = true;
		// since the more natural means of causing passThrough are taking over now, no need for the manual override
		manuallyPassThrough = false;
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		passThrough = true;
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		passThrough = false;
	}
}