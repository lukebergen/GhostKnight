using UnityEngine;
using System.Collections;

public class ProtagController : MonoBehaviour {
	
	public float maxSpeed = 5.0f;
	public bool grounded = false;
	public bool almostGrounded = false;
	public Transform almostGroundedCheck;
	public float almostGroundedRadius = 0.16f;
	public Transform groundCheckTopLeft;
	public Transform groundCheckBottomRight;
	public LayerMask whatIsGround;
	public float jumpVelocity = 2.5f;
	public float maxSlopeDotProduct = 0.69f;//0.7f is roughly a 45° angle - 0 is horizontal, 1 is vertical
	public float groundedCheckDistance = 0.015f;
	public int direction = -1;

	private Animator anim;
	private Transform thisTransform;
	private BoxCollider2D thisBoxCollider2D;
	private Vector2 targetVelocity;
	private RaycastHit2D[] groundCastHit = new RaycastHit2D[1];
	
	// Use Awake() for initialization of local references
	void Awake () {
		anim = GetComponent<Animator>();
		thisTransform = GetComponent<Transform>();
		thisBoxCollider2D = GetComponent<BoxCollider2D>();
	}
	
	// Use FixedUpdate() for Physics stuff
	void FixedUpdate() {
		//Check if grounded, and grab ground data
		grounded = GroundedCheck();
		//grounded = Physics2D.OverlapArea (groundCheckTopLeft.position, groundCheckBottomRight.position,  whatIsGround);

		//Jump Input
		almostGrounded = Physics2D.OverlapCircle (almostGroundedCheck.position, almostGroundedRadius, whatIsGround);

		//Horizontal Input
		float move = Input.GetAxis ("Horizontal");

		//Set Velocity based on Inputs
		targetVelocity = rigidbody2D.velocity;
		targetVelocity.x = move * maxSpeed;
		
		if (grounded) {
			if (Input.GetButtonDown ("Jump")) //I like "GetButtonDown over GetAxis because holding the button doesn't yield repeaded jumping - as you should =-)
			{
				targetVelocity.y = jumpVelocity;//Use velocity for jumps instead of force - yields more consistent jump heights
				grounded = false;
				rigidbody2D.velocity = targetVelocity;
			}
			else
			{
				targetVelocity.y = 0;
				targetVelocity -= groundCastHit[0].normal;
				GameObject groundObject = groundCastHit[0].collider.gameObject;

				if (Input.GetButton("Crouch")) {
					OneWayPlatformTrigger owpt = groundObject.GetComponentInChildren<OneWayPlatformTrigger>();
					if (owpt) {
						owpt.manuallyPassThrough = true;
					}
				}

				Rigidbody2D groundBody = groundObject.GetComponent<Rigidbody2D>();
				if (groundBody != null) {
					targetVelocity += groundBody.velocity;
				}
				rigidbody2D.MovePosition(rigidbody2D.position + targetVelocity * Time.fixedDeltaTime);
			}
		}

		rigidbody2D.velocity = targetVelocity;
		
		//Set Animation Variables
		anim.SetBool ("Grounded", grounded);
		anim.SetBool ("AlmostGrounded", almostGrounded);
		anim.SetFloat ("Speed", Mathf.Abs (move));
		//anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);//Commented out because it doesn't exist in the animation controller and was throwing warnings
		
		//Set Facing Direction
		if ((move > 0 && direction == -1) || (move < 0 && direction == 1)) {
			Flip ();
		}
	}
	
	void Flip() {
		direction *= -1;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		thisTransform.localScale = theScale;
	}

	//-------------------------------------

	private bool GroundedCheck ()
	{
		if (Physics2D.BoxCastNonAlloc(rigidbody2D.position + Vector2.right * thisBoxCollider2D.center.x * thisTransform.localScale.x + Vector2.up * thisBoxCollider2D.center.y, thisBoxCollider2D.size, 0f, -Vector2.up, groundCastHit, groundedCheckDistance) > 0)
		{
			if (Vector2.Dot(Vector2.up, groundCastHit[0].normal) >= maxSlopeDotProduct)
			{
				return true;
			}
		}

		return false;
	}
	
	//-------------------------------------
	
	#if UNITY_EDITOR
	//The #if UNITY_EDITOR tag makes sure this code doesn't get included with builds
	void OnDrawGizmos ()
	{
		Gizmos.color = almostGrounded ? Color.cyan : Color.red;
		for (int i = 0; i < 32; i++)
		{
			Gizmos.DrawLine(Quaternion.Euler(Vector3.forward * (360f/32f * i)) * Vector3.up * almostGroundedRadius + almostGroundedCheck.position, Quaternion.Euler(Vector3.forward * (360f/32f * i + 360f/64f)) * Vector3.up * almostGroundedRadius + almostGroundedCheck.position);
		}
		
		Gizmos.color = grounded ? Color.cyan : Color.red;
		Gizmos.DrawLine(groundCheckTopLeft.position, Vector3.up * groundCheckTopLeft.position.y + Vector3.right * groundCheckBottomRight.position.x);
		Gizmos.DrawLine(Vector3.up * groundCheckTopLeft.position.y + Vector3.right * groundCheckBottomRight.position.x, groundCheckBottomRight.position);
		Gizmos.DrawLine(groundCheckBottomRight.position, Vector3.up * groundCheckBottomRight.position.y + Vector3.right * groundCheckTopLeft.position.x);
		Gizmos.DrawLine(Vector3.up * groundCheckBottomRight.position.y + Vector3.right * groundCheckTopLeft.position.x, groundCheckTopLeft.position);
	}
	#endif
}
