using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementAnimations : MonoBehaviour {

	public string CurrentMovementAnimation;
	public Dictionary<string, object> Arguments = new Dictionary<string, object>();
	public Dictionary<string, object> State = new Dictionary<string, object>();

	// Use this for initialization
	void Start () {
		CurrentMovementAnimation = "";
	}
	
	// Update is called once per frame
	void Update () {
		switch (CurrentMovementAnimation) {
		case "WalkBackAndForth":
			tickWalkBackAndForth();
			break;
		}
	}

	public void SetAnimation(string name, Dictionary<string, object> arguments) {
		CurrentMovementAnimation = name;
		Arguments = arguments;
		switch (name) {
		case "WalkBackAndForth":
			initWalkBackAndForth(arguments);
			tickWalkBackAndForth();
			break;
		}
	}

	private void initWalkBackAndForth(Dictionary<string, object> arguments) {
		State["lastTurnAround"] = Time.time;
		if (arguments.ContainsKey("initialDirection")) {
			State["direction"] = arguments["initialDirection"];
		} else {
			State["direction"] = -1;
		}
	}

	private void tickWalkBackAndForth() {
		if (((float)State["lastTurnAround"]) + ((float)Arguments["period"]) <= Time.time) {
			GameObject subject = (GameObject)Arguments["subject"];
			CharacterController cc = subject.GetComponent<CharacterController>();
			cc.Walk((int)State["direction"]);
			State["direction"] = ((int)State["direction"]) * -1;
			State["lastTurnAround"] = Time.time;
		}
	}
}
