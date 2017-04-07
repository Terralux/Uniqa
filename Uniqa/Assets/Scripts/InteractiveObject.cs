using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour {
	public abstract void Interact (Vector3 targetPosition);
	public abstract void Initialize (Vector3 targetPosition);
	public abstract void End (Vector3 targetPosition);
}
