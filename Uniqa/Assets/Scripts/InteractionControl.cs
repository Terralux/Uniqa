using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControl : MonoBehaviour {

    [HideInInspector]
	public bool canInteract = false;

    [HideInInspector]
	public static InteractiveObject io;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Start(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () {
	    if (io == null)
	    {
	        return;
	    }

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
		    Debug.Log("I found an interactive object!");
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.GetComponent<InteractiveObject>() != null) {
			canInteract = false;
			io = null;
		}
	}
}