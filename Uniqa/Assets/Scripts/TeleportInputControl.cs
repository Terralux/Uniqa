using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInputControl : MonoBehaviour {

	public float teleportMarkerHeightOffset = 0.1f;

	public Transform playArea;

	private float heightOffset = 1.6f;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	private Transform teleportTarget;

	private Vector3 teleportLocation;

	private bool canTeleport = true;
	private bool teleportEnabled = true;


	void Awake(){
		teleportTarget = transform.GetChild (0);
		teleportTarget.SetParent (null);

		if (teleportTarget == null) {
			Debug.LogError ("The Teleport target is missing, please add a child to handle Transform");
		}

		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	// Update is called once per frame
	void Update () {
		if (teleportEnabled) {
			RaycastHit hit;
			Ray ray = new Ray (trackedObj.transform.position, trackedObj.transform.forward);

			if (Physics.Raycast (ray, out hit, 100f)) {

				if (hit.collider.gameObject.GetComponent<InteractiveObject>() == null) {

					teleportTarget.position = hit.point + new Vector3 (0, teleportMarkerHeightOffset, 0);

					if (Controller.GetHairTriggerDown () && canTeleport) {
						teleportLocation = hit.point;
						canTeleport = false;
						MovePlayer ();
					}
				}
			}
		} else {
			teleportTarget.position = new Vector3 (100000, 100000, 100000);
		}
	}

	void MovePlayer(){
		Vector3 difference = playArea.position - transform.position;
		difference = new Vector3 (difference.x, 0, difference.z);
		playArea.position = teleportLocation + new Vector3 (0, heightOffset, 0) + difference;
		canTeleport = true;
	}
}