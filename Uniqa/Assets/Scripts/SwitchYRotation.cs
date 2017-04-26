using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchYRotation : InteractiveObject
{
	
    private float currentRotation;

	//This determines the speed/range of the rotation setup
    private float maxDistance = 1f;

    #region implemented abstract members of InteractiveObject

    public override void Interact (Vector3 targetPosition)
    {
        if (InteractiveChangableObject.currentlySelected != null)
        {
			//calculates a rotation based on how far the controller has been dragged from the center
            float axisRotation = Vector3.Distance(transform.position, targetPosition) / maxDistance;

			//Determines whether the controller is moving left or right
            if (Vector3.Distance((transform.position - targetPosition).normalized, transform.right) > Vector3.Distance(
                    (transform.position - targetPosition).normalized, -transform.right))
            {
                axisRotation *= -1;
            }

			//Rotates the object
            InteractiveChangableObject.currentlySelected.GetComponent<InteractiveChangableObject>().Rotate(axisRotation);
        }
    }

    public override void Initialize (Vector3 targetPosition)
    {

    }

	//Upon end reset the interactive object
    public override void End (Vector3 targetPosition){
        InteractionControl.io = null;
    }

    #endregion

}