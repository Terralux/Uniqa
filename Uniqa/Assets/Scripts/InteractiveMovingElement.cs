using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMovingElement : InteractiveObject {

	public Transform[] movePoints;
	public List<MovableObjectNode> nodes = new List<MovableObjectNode> ();

	private int closestPoint = 0;
	private Transform target;

	#region implemented abstract members of InteractiveObject

	void Start(){
		foreach (Transform t in movePoints) {
			nodes.Add(t.GetComponent<MovableObjectNode> ());
		}
	}

	public override void Initialize (Vector3 targetPosition){
		foreach (Transform t in movePoints) {
			t.SetParent (null);
		}
		FindClosestPoint (targetPosition);
	}

	public override void Interact (Vector3 targetPosition){
		FindClosestPoint (targetPosition);
		transform.position = nodes [closestPoint].GetLerpedLocation (targetPosition);
	}

	#endregion

	private void FindClosestPoint(Vector3 targetPosition){
		closestPoint = 0;

		for (int i = 1; i < movePoints.Length; i++) {
			if (Vector3.Distance (targetPosition, movePoints[closestPoint].position) > Vector3.Distance (targetPosition, movePoints [i].position)) {
				closestPoint = i;
			}
		}
	}

	public override void End (Vector3 targetPosition){

	}
}