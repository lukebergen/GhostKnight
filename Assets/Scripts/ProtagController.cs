using UnityEngine;
using System.Collections;

public class ProtagController : MonoBehaviour {

	public float maxSpeed = 5.0f;
	private bool facingRight = false;

	private Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public float jumpForce = 700.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Physics2D.IgnoreLayerCollision( LayerMask.NameToLayer("Player"), 
		                               LayerMask.NameToLayer("Ground"), 
		                               rigidbody2D.velocity.y > 0
		                               );

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		anim.SetBool ("Grounded", grounded);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if ((move > 0 && !facingRight) || (move < 0 && facingRight)) {
			Flip ();
		}
	}

	void Update() {
		if (grounded && Input.GetAxis ("Jump") > 0) {
			grounded = false;
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
