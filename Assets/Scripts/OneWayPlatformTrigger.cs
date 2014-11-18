using UnityEngine;
using System.Collections;

public class OneWayPlatformTrigger : MonoBehaviour {
	
	public bool oneWay = false;

	private Collider2D platform;
	
	// Use this for initialization
	void Start () {
		platform = transform.parent.gameObject.GetComponent<EdgeCollider2D>();
		if (platform == null) { platform = transform.parent.gameObject.GetComponent<BoxCollider2D>(); }
		if (platform == null) { platform = transform.parent.gameObject.GetComponent<PolygonCollider2D>(); }
		if (platform == null) { platform = transform.parent.gameObject.GetComponent<CircleCollider2D>(); }
	}
	
	// Update is called once per frame
	void Update () 
	{
		platform.enabled = !oneWay; 
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		oneWay = true;
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		oneWay = true;
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		oneWay = false;
	}
}