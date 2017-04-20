using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour {
    //Used for initializing an interaction
    //e.g. pickup a ball, reparent the ball and disable physics on it
    public abstract void Initialize (Vector3 targetPosition);

    //Used for handling object during interaction
    //e.g. calculate force and direction of ball
	public abstract void Interact (Vector3 targetPosition);

    //Used for resolving the interaction
    //e.g. finalize the force, unparent and enable physics for the ball
    public abstract void End (Vector3 targetPosition);
}
