using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class CharacterController : MonoBehaviour {

	public enum State {
		Walking,
		Running,
		Idle
	}

	public int direction = 1;
	public ArrayList states = new ArrayList();
	public float WalkSpeed;
	public float RunSpeed;

	void Start() {
		states.Add (State.Idle);
	}

	void FixedUpdate () {
		Vector2 vel = rigidbody2D.velocity;
		if (states.IndexOf(State.Walking) != -1) {
			vel.x = WalkSpeed * direction;
			rigidbody2D.velocity = vel;
		}
		if (states.IndexOf(State.Running) != -1) {
			vel.x = RunSpeed * direction;
			rigidbody2D.velocity = vel;
		}
	}

	public void Idle() {
		SetState(State.Idle);
	}

	public void Turn(string newDir) {
		if (newDir == "left") {
			direction = -1;
		}
		if (newDir == "right") {
			direction = 1;
		}
	}

	public void Walk(string dir) {
		Turn (dir);
		SetState (State.Walking);
	}

	public void Walk(int newDir) {
		direction = newDir;
		SetState (State.Walking);
	}

	public void Run(string dir) {
		Turn (dir);
		SetState (State.Running);
	}

	public void Run(int newDir) {
		direction = newDir;
		SetState (State.Running);
	}

	public void SetState(State newState) {
		// Currently "walking", "running", and "idle" are all mutually exclusive.
		// But at some point we may want creatures to be able to be both "hunting" and "walking" for instance
		states = new ArrayList();
		states.Add(newState);
	}

	public void Hide() {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
}
