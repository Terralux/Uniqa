using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSwitchObject : InteractiveObject {

    #region implemented abstract members of InteractiveObject

    public override void Interact (Vector3 targetPosition)
    {
        if (InteractiveChangableObject.currentlySelected != null)
        {
            InteractiveChangableObject.currentlySelected.GetComponent<InteractiveChangableObject>().ChangeObject();
            Debug.Log("I Changed an Object");
            InteractionControl.io = null;
        }
    }

    public override void Initialize (Vector3 targetPosition){

    }

    public override void End (Vector3 targetPosition){

    }

    #endregion

}