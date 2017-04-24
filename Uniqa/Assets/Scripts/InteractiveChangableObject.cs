using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveChangableObject : InteractiveObject
{

    private InstancedObject myIOScript;
    public static GameObject currentlySelected;

	private float rotationSpeedModifier = 3f;

    void Awake()
    {
        myIOScript = transform.parent.GetComponent<InstancedObject>();
    }

    #region implemented abstract members of InteractiveObject

    public override void Interact (Vector3 targetPosition){
        Debug.Log("I got picked!");
        currentlySelected = gameObject;
    }

    public override void Initialize (Vector3 targetPosition){

    }

    public override void End (Vector3 targetPosition){

    }

    #endregion

    public void ChangeColor()
    {
        if (myIOScript.currentObjectInfo.currentColorIndex < (myIOScript.currentObjectInfo._category.GetElementAt(myIOScript.currentObjectInfo.currentObjectIndex) as BaseObject).GetLength() - 1)
        {
            myIOScript.currentObjectInfo.currentColorIndex++;
        }
        else
        {
            myIOScript.currentObjectInfo.currentColorIndex = 0;
        }

        myIOScript.Initialize(myIOScript.currentObjectInfo._category, myIOScript.currentObjectInfo.currentObjectIndex,
            myIOScript.currentObjectInfo.currentColorIndex, myIOScript.currentObjectInfo.yRotation);
    }

    public void ChangeObject()
    {
        if (myIOScript.currentObjectInfo.currentObjectIndex < myIOScript.currentObjectInfo._category.GetLength() - 1)
        {
            myIOScript.currentObjectInfo.currentObjectIndex++;
        }
        else
        {
            myIOScript.currentObjectInfo.currentObjectIndex = 0;
        }

        myIOScript.Initialize(myIOScript.currentObjectInfo._category, myIOScript.currentObjectInfo.currentObjectIndex,
            myIOScript.currentObjectInfo.currentColorIndex, myIOScript.currentObjectInfo.yRotation);
    }

    public void Rotate(float yAxisRotation)
    {
		transform.Rotate(new Vector3(0, yAxisRotation * rotationSpeedModifier, 0));
        myIOScript.SampleRotation();
    }
}