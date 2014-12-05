using UnityEngine;
using System.Collections;

public class ClimbingOneMovement : MonoBehaviour {

	private Animator animator;
	public bool updatePosition;
	public bool playMovementAnim;
	public bool move;
	public bool readyToMove = false;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/* • If 'move' (set from an AI script)
		 * • Play the movement animation
		 * • When the animation is complete, update transform.position to "teleport" the transform up 0.63 units
		 * • Update "readyToMove" so if the AI calls for future movement, it can occur.
		 * */

		/*
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		if(move && stateInfo.nameHash == Animator.StringToHash("ClimbingOneClimbing")) {
			readyToMove = false;
		}
		if(move && stateInfo.nameHash != Animator.StringToHash("ClimbingOneClimbing")) {
			readyToMove = true;
		}
		*/
	}
}