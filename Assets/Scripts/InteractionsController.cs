using UnityEngine;
using System.Collections;

public class InteractionsController : MonoBehaviour {

	public enum InteractionType{Cuttable, Pokeable};

	public InteractionType[] myInteractionTypes;

	public bool hasType(InteractionType type) {
		for (int i = 0 ; i < this.myInteractionTypes.Length ; i++) {
			if (myInteractionTypes[i] == type) {
				return true;
			}
		}
		return false;
	}
}
