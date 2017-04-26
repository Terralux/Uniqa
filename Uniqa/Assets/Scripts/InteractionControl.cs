using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControl : MonoBehaviour {

	//determines whether the player can interact with an object or not
    [HideInInspector]
	public bool canInteract = false;

	//contains the interactive object to e interacted with
    [HideInInspector]
	public static InteractiveObject io;

	//the tracked HTC Vive object 
	private SteamVR_TrackedObject trackedObj;

	//sets up a getter for the controller at a specific index
	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	//gets a reference to the specific tracked object that's attached to this GameObject
	void Start(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	//Handles interaction with the interactive object
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

	//Used to determine if an interactive object is within trigger area
	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<InteractiveObject>() != null) {
			canInteract = true;
			io = col.gameObject.GetComponent<InteractiveObject> ();
		    Debug.Log("I found an interactive object!");
		}
	}

	//used when the interactive object leaves the trigger area
	void OnTriggerExit(Collider col){
		if (col.gameObject.GetComponent<InteractiveObject>() != null) {
			canInteract = false;
			io = null;
		}
	}
}