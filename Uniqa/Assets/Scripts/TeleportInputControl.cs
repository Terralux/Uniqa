using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

/// <summary>
/// Teleport input control.
/// This controls the Teleport behaviour
/// </summary>
public class TeleportInputControl : MonoBehaviour {

	/// <summary>
	/// The teleport marker height offset.
	/// </summary>
	public float teleportMarkerHeightOffset = 0.1f;

	//This is the HTC Vive VR area gameobject
	public Transform playArea;

	/// <summary>
	/// The height offset of the VR area.
	/// </summary>
	private float heightOffset = 1.6f;

	//This is the teleport icon used to show you where you are teleporting
	private Transform teleportTarget;

	//current teleport location
	private Vector3 teleportLocation;

	//This value can be used to control whether the player can teleport in certain situations
    [HideInInspector]
	public bool canTeleport = true;
	public bool teleportEnabled = true;

	//used to get a reference to the teleport target
	void Awake(){
		teleportTarget = transform.GetChild (0);
		teleportTarget.SetParent (null);

		if (teleportTarget == null) {
			Debug.LogError ("The Teleport target is missing, please add a child to handle Teleportation");
		}
	}

	//This is used to move the teleportTarget offscreen
	void Update () {
		if (!teleportEnabled) {
			teleportTarget.position = new Vector3 (100000, 100000, 100000);
		}
	}

	//Used to move player to a specific target location
	public void MovePlayer(Vector3 target){
	    teleportLocation = target;

		Vector3 difference = playArea.position - transform.position;
		difference = new Vector3 (difference.x, 0, difference.z);
		playArea.position = teleportLocation + new Vector3 (0, heightOffset, 0) + difference;
	}

	//sets the teleport target at a target location
    public void SetTeleportPosition(Vector3 target)
    {
        teleportTarget.position = target + Vector3.up * heightOffset;
    }
}