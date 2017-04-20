using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControl : MonoBehaviour {

	private bool canInteract = false;

	private InteractiveObject io;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Start(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canInteract) {
			if (Controller.GetHairTriggerDown ()) {
				io.Initialize (transform.position);
			}

			if (Controller.GetHairTrigger()){
				io.Interact (transform.position);
			}

			if (Controller.GetHairTriggerUp ()) {
				io.End (transform.position);
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<InteractiveObject>() != null) {
			canInteract = true;
			io = col.gameObject.GetComponent<InteractiveObject> ();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.GetComponent<InteractiveObject>() != null) {
			canInteract = false;
			io = null;
		}
	}
}