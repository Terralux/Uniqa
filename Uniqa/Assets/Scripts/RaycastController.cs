using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The Raycast controller is basically the root of how the controller system works
//As the Raycast controller is the culmination of every interaction, each interaction setup should be integrated here
public class RaycastController : MonoBehaviour {

	//A tracked object from the HTC Vive e.g. a controller
    private SteamVR_TrackedObject trackedObj;

	//Sets up a getter for a specific controller based on the controller index
    private SteamVR_Controller.Device Controller{
        get { return SteamVR_Controller.Input ((int)trackedObj.index); }
    }

	//reference to teleport controller used for handling teleportation
    private TeleportInputControl teleportController;
	//reference to interaction controller used for interacting
    //private InteractionControl interactionController;

    private InteractiveLoadScene previousLoadObject;

    //used to initialize the variables
	void Awake ()
	{
	    teleportController = GetComponent<TeleportInputControl>();
	    trackedObj = GetComponent<SteamVR_TrackedObject> ();
	    //interactionController = GetComponent<InteractionControl>();
	}

	//used to determine what the controller is currently pointing at
	void Update ()
	{
		//we create a ray and a hit variable
	    RaycastHit hit;
	    Ray ray = new Ray(trackedObj.transform.position, trackedObj.transform.forward);

		//we do a physics raycast to see if there's something in front of the controller
	    if (Physics.Raycast(ray, out hit, 100f))
	    {

	        if (previousLoadObject != null)
	        {
	            previousLoadObject.End(Vector3.zero);
	            previousLoadObject = null;
	        }

	        //Are we looking at an interactive object or not, handle interaction or teleportation
	        if (hit.collider.gameObject.GetComponent<InteractiveObject>() == null)
	        {
	            if (teleportController.teleportEnabled)
	            {
	                //we move the teleport marker to the hit position of our raycast
	                teleportController.SetTeleportPosition(hit.point);

	                //we check if the analog trigger is being pressed down in this frame
	                if (Controller.GetHairTriggerDown())
	                {
	                    //teleport player to hit position
	                    teleportController.MovePlayer(hit.point);
	                }
	            }
	        }
	        else
	        {
	            //we hit an interactive object, we're trying to get the base class InteractiveObject to get it's derived class
	            InteractiveObject io = hit.collider.gameObject.GetComponent<InteractiveObject>();

	            //If the object is of the changable type, we initialize the object
	            if (io as InteractiveChangableObject != null)
	            {
	                (io as InteractiveChangableObject).Initialize(Vector3.zero);

	                //If the analog trigger is being pressed down in this frame
	                if (Controller.GetHairTriggerDown())
	                {
	                    //we call the changable objects interact method
	                    (io as InteractiveChangableObject).Interact(Vector3.zero);
	                }
	            }

	            if (io as InteractiveLoadScene != null)
	            {
	                previousLoadObject = io as InteractiveLoadScene;

	                (io as InteractiveLoadScene).Initialize(Vector3.zero);

	                //If the analog trigger is being pressed down in this frame
	                if (Controller.GetHairTriggerDown())
	                {
	                    //we call the changable objects interact method
	                    (io as InteractiveLoadScene).Interact(Vector3.zero);
	                }
	            }
	        }
	    }
	    else
	    {

	        if (previousLoadObject != null)
	        {
	            previousLoadObject.End(Vector3.zero);
	            previousLoadObject = null;
	        }

	    }
	}
}