using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TeleportInputControl : MonoBehaviour {

	public float teleportMarkerHeightOffset = 0.1f;

	public Transform playArea;

	private float heightOffset = 1.6f;

	private Transform teleportTarget;

	private Vector3 teleportLocation;

    [HideInInspector]
	public bool canTeleport = true;
	public bool teleportEnabled = true;

	void Awake(){
		teleportTarget = transform.GetChild (0);
		teleportTarget.SetParent (null);

		if (teleportTarget == null) {
			Debug.LogError ("The Teleport target is missing, please add a child to handle Transform");
		}
	}

	void Update () {
		if (!teleportEnabled) {
			teleportTarget.position = new Vector3 (100000, 100000, 100000);
		}
	}

	public void MovePlayer(Vector3 target){
	    teleportLocation = target;
	    canTeleport = false;

		Vector3 difference = playArea.position - transform.position;
		difference = new Vector3 (difference.x, 0, difference.z);
		playArea.position = teleportLocation + new Vector3 (0, heightOffset, 0) + difference;
		canTeleport = true;
	}

    public void SetTeleportPosition(Vector3 target)
    {
        teleportTarget.position = target + Vector3.up * heightOffset;
    }
}