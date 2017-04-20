using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractiveFollowObject : InteractiveObject {

	private Vector3 offset;

	private Rigidbody rb;

    private List<Vector3> directions = new List<Vector3>(30);

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

    void ResetDirections()
    {
        for (int i = 0; i < 30; i++)
        {
            directions[i] = Vector3.zero;
        }
    }

    #region implemented abstract members of InteractiveObject

	public override void Interact (Vector3 targetPosition){
	    for (int i = 29; i > 0; i--)
	    {
	        directions[i] = directions[i - 1];
	    }

	    directions[0] = transform.position - directions[0];
	}

	public override void Initialize (Vector3 targetPosition){

	    ResetDirections();

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
	    rb.constraints = RigidbodyConstraints.FreezeAll;

	    directions[0] = transform.position;
	}

	public override void End (Vector3 targetPosition) {
		transform.SetParent (null);
		rb.useGravity = true;

	    rb.constraints = RigidbodyConstraints.None;

	    Vector3 direction = Vector3.zero;
	    foreach (Vector3 v in directions)
	    {
	        direction += v;
	    }

		rb.velocity = direction * (1/30);
	}

	#endregion
}