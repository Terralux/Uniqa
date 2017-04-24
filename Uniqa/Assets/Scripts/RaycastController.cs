using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller{
        get { return SteamVR_Controller.Input ((int)trackedObj.index); }
    }

    private TeleportInputControl teleportController;
    private InteractionControl interactionController;

	void Awake ()
	{
	    teleportController = GetComponent<TeleportInputControl>();
	    trackedObj = GetComponent<SteamVR_TrackedObject> ();
	    interactionController = GetComponent<InteractionControl>();
	}

	void Update ()
	{
	    RaycastHit hit;
	    Ray ray = new Ray(trackedObj.transform.position, trackedObj.transform.forward);

	    if (Physics.Raycast(ray, out hit, 100f))
	    {

	        if (hit.collider.gameObject.GetComponent<InteractiveObject>() == null)
	        {
				if (teleportController.canTeleport && teleportController.teleportEnabled)
	            {
	                teleportController.SetTeleportPosition(hit.point);

	                if (Controller.GetHairTriggerDown())
	                {
	                    teleportController.MovePlayer(hit.point);
	                }
	            }
	        }
	        else
	        {
	            InteractiveObject io = hit.collider.gameObject.GetComponent<InteractiveObject>();

	            if (io as InteractiveChangableObject != null)
	            {
	                (io as InteractiveChangableObject).Initialize(Vector3.zero);

	                if (Controller.GetHairTriggerDown())
	                {
	                    (io as InteractiveChangableObject).Interact(Vector3.zero);
	                }
	            }
	        }
	    }
	}
}