using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactive follow object requires a Rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class InteractiveFollowObject : InteractiveObject {

	//the offset from the controller to the center of the object
	private Vector3 offset;

	//the rigidbody of the object
	private Rigidbody rb;

	//storing a list of vectors used to determine the throwing direction
    private List<Vector3> directions = new List<Vector3>(30);

	//Gets the rigidbody
	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	/// <summary>
	/// Resets the directions list.
	/// </summary>
    void ResetDirections()
    {
        for (int i = 0; i < 30; i++)
        {
            directions[i] = Vector3.zero;
        }
    }

    #region implemented abstract members of InteractiveObject

	//As long as the players interacts, the object will keep storing new direction vectors
	public override void Interact (Vector3 targetPosition){
	    for (int i = 29; i > 0; i--)
	    {
	        directions[i] = directions[i - 1];
	    }

	    directions[0] = transform.position - directions[0];
	}

	//This setup could use some refinements
	//gets the closest controller, locks the rigidbody and reparents this object to the controller
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

	//Calculates direction and launches this object
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