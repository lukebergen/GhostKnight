using UnityEngine;
using System.Collections;

public class ProtagController : MonoBehaviour {

	public float maxSpeed = 5.0f;
	private bool facingRight = false;

	private Animator anim;

	public bool grounded = false;
	public bool almostGrounded = false;
	public Transform almostGroundedCheck;
	public float almostGroundedRadius = 0.16f;
	public Transform groundCheckTopLeft;
	public Transform groundCheckBottomRight;
	public LayerMask whatIsGround;

	public float jumpForce = 150.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*
		Physics2D.IgnoreLayerCollision( LayerMask.NameToLayer("Player"), 
		                               LayerMask.NameToLayer("Ground"), 
		                               rigidbody2D.velocity.y > 0 && !grounded
		                               );
		*/

	}



	void Update() {
		// Jump
		grounded = Physics2D.OverlapArea (groundCheckTopLeft.position, groundCheckBottomRight.position,  whatIsGround);
		almostGrounded = Physics2D.OverlapCircle (almostGroundedCheck.position, almostGroundedRadius, whatIsGround);
		
		anim.SetBool ("Grounded", grounded);
		anim.SetBool ("AlmostGrounded", almostGrounded);
		
		float move = Input.GetAxis ("Horizontal");
		
		anim.SetFloat ("Speed", Mathf.Abs (move));
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		
		if ((move > 0 && !facingRight) || (move < 0 && facingRight)) {
			Flip ();
		}

		if (grounded && Input.GetButtonDown ("Jump")) { 			//I like "GetButtonDown over GetAxis because holding the button doesn't yield repeaded jumping 
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
			grounded = false;
		}

	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
