using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchYRotation : InteractiveObject
{

    private float currentRotation;

    private Transform player;
    private float maxDistance = 1f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #region implemented abstract members of InteractiveObject

    public override void Interact (Vector3 targetPosition)
    {
        if (InteractiveChangableObject.currentlySelected != null)
        {
            float axisRotation = Vector3.Distance(transform.position, targetPosition) / maxDistance;

            if (Vector3.Distance((transform.position - targetPosition).normalized, transform.right) > Vector3.Distance(
                    (transform.position - targetPosition).normalized, -transform.right))
            {
                //moving left
                axisRotation *= -1;
            }

            InteractiveChangableObject.currentlySelected.GetComponent<InteractiveChangableObject>().Rotate(axisRotation);
        }
    }

    public override void Initialize (Vector3 targetPosition)
    {

    }

    public override void End (Vector3 targetPosition){
        InteractionControl.io = null;
    }

    #endregion

}