using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionControl : MonoBehaviour {

	public Transform playArea;

	public Transform KompanLogoGuy;
	public float heightOffset = 2f;

	private SteamVR_TrackedObject trackedObj;

	private bool isInFocus = false;

	private LookTarget prevLt;
	private LookTarget lt;

	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () {
		RaycastHit hit = new RaycastHit();
		Ray ray = new Ray (trackedObj.transform.position, trackedObj.transform.forward);

		isInFocus = false;

		if (Physics.Raycast (ray, out hit, 100f)) {
			if (hit.collider != null) {
				prevLt = lt;
				lt = hit.collider.gameObject.GetComponent<LookTarget> ();

				if (lt != null) {
					if ((lt as WobbleTarget).isActiveAndEnabled) {
						isInFocus = true;
						lt.Focus (isInFocus);
						KompanLogoGuy.position = hit.collider.transform.position + Vector3.up * heightOffset;

						if (lt != prevLt) {
							if (prevLt != null) {
								prevLt.Focus (false);
								KompanLogoGuy.position = new Vector3 (0, -10000, 0);
							}
						}
					}

					if (Controller.GetHairTriggerDown ()) {
						lt.Action ();
						KompanLogoGuy.position = new Vector3 (0, -10000, 0);
					}
				} else {
					if (prevLt != null) {
						prevLt.Focus (false);
						KompanLogoGuy.position = new Vector3 (0, -10000, 0);
					}
				}
			} else {
				if (prevLt != null) {
					prevLt.Focus (false);
					KompanLogoGuy.position = new Vector3 (0, -10000, 0);
				}
			}
		}
	}
}