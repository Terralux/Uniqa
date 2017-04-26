using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSwitchColor : InteractiveObject {

    #region implemented abstract members of InteractiveObject

	//Upon interactions, calls the currently selected objects changecolor method, then sets the interactive object of the interaction control to null
    public override void Interact (Vector3 targetPosition)
    {
        if (InteractiveChangableObject.currentlySelected != null)
        {
            InteractiveChangableObject.currentlySelected.GetComponent<InteractiveChangableObject>().ChangeColor();
            Debug.Log("I Changed Color");
            InteractionControl.io = null;
        }
    }

    public override void Initialize (Vector3 targetPosition){

    }

    public override void End (Vector3 targetPosition){

    }

    #endregion

}