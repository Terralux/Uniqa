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
	private bool teleporterIsOn = true;

	public GameObject teleportIcon;
	public MeshRenderer teleportMesh;

	public LineRenderer lr;

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
		if (teleporterIsOn) {
			RaycastHit hit;
			Ray ray = new Ray (trackedObj.transform.position, trackedObj.transform.forward);

			if (Physics.Raycast (ray, out hit, 100f)) {

				Vector3[] newPoints = Curver.MakeSmoothCurve (new Vector3[]{ transform.position, hit.point }, 3f);
				lr.SetPositions (newPoints);

				//lr.SetPosition (0, transform.position);
				//lr.SetPosition (1, hit.point);

				if (hit.collider.gameObject.CompareTag ("Terrain")) {

					teleportTarget.position = hit.point + new Vector3 (0, teleportMarkerHeightOffset, 0);

					teleportMesh.material.SetColor ("_TintColor", new Color (0, 1, 0, 1));

					if (Controller.GetHairTriggerDown () && canTeleport) {
						//fm.Fade (1);
						//fm.OnCompletelyFaded += MovePlayer;
						teleportLocation = hit.point;
						canTeleport = false;
						MovePlayer ();
					}
				} else if (hit.collider.gameObject.CompareTag ("Exit")) {
					teleportTarget.position = hit.point + new Vector3 (0, teleportMarkerHeightOffset, 0);

					teleportMesh.material.SetColor ("_TintColor", new Color (1, 0, 0, 1));

					if (Controller.GetHairTriggerDown () && canTeleport) {
						if (!SteamVR_LoadLevel.loading) {
							SteamVR_LoadLevel.Begin ("Kompan Test Scene", false, 0.5f, 0, 0, 0, 1);
						}
					}
				} else if (hit.collider.gameObject.CompareTag ("Island")) {
					teleportTarget.position = hit.point + new Vector3 (0, teleportMarkerHeightOffset, 0);

					teleportMesh.material.SetColor ("_TintColor", new Color (1, 0, 1, 1));

					if (Controller.GetHairTriggerDown () && canTeleport) {
						playArea.position = hit.collider.GetComponent<TeleportToNextIsland> ().teleportLocation.position;
					}
				} else if (hit.collider.gameObject.CompareTag ("Popup")) {
					if (Controller.GetHairTriggerDown ()) {
						hit.collider.gameObject.GetComponent<InteractivePopup> ().Initialize (new Vector3 (0, 0, 0));
					}
				}
			} else {
				lr.SetPosition (0, transform.position);
				lr.SetPosition (1, transform.position + transform.forward * 100);
			}
		} else {
			teleportTarget.position = new Vector3 (100000, 100000, 100000);
			lr.SetPosition (0, transform.position);
			lr.SetPosition (1, transform.position + transform.forward * 100);
		}
	}

	void MovePlayer(){
		Vector3 difference = playArea.position - transform.position;
		difference = new Vector3 (difference.x, 0, difference.z);
		playArea.position = teleportLocation + new Vector3 (0, heightOffset, 0) + difference;
		canTeleport = true;
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.CompareTag ("Terrain")) {
			GetComponent<Rigidbody> ().useGravity = false;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Interactive")) {
			teleporterIsOn = false;
			teleportTarget.position = new Vector3 (100000, -100000, 100000);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.CompareTag ("Interactive")) {
			teleporterIsOn = true;
		}
	}

	/*
	 * info bokse
	*/
}