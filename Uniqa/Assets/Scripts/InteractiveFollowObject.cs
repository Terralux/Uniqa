using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveFollowObject : InteractiveObject {

	public Transform goal;
	public float speed = 3f;

	private Vector3 offset;

	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	#region implemented abstract members of InteractiveObject

	public override void Interact (Vector3 targetPosition){
		
	}

	public override void Initialize (Vector3 targetPosition){

		rb.velocity = Vector3.zero;

		GameObject[] GOs = GameObject.FindGameObjectsWithTag ("Controller");

		GameObject targetGO = GOs[0];

		for (int i = 1; i < GOs.Length; i++) {
			if (Vector3.Distance (transform.position, targetGO.transform.position) > Vector3.Distance (transform.position, GOs [i].transform.position)) {
				targetGO = GOs [i];
			}
		}

		transform.SetParent (targetGO.transform);
		rb.useGravity = false;
	}

	public override void End (Vector3 targetPosition) {
		transform.SetParent (null);
		rb.useGravity = true;
		rb.velocity = (goal.position + (Vector3.up * speed / 2) - transform.position).normalized * speed;
	}

	#endregion
}