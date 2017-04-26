using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveChangableObject : InteractiveObject
{
	//reference to the InteractiveObject Script
    private InstancedObject myIOScript;
	//used to determine which object has been selected
    public static GameObject currentlySelected;

	//The speed at which an object spins based on the input
	private float rotationSpeedModifier = 3f;

	//Upon being added to the object this component will look for the component that spawned it to update the data of itself
    void Awake()
    {
        myIOScript = transform.parent.GetComponent<InstancedObject>();
    }

    #region implemented abstract members of InteractiveObject

	//on interaction, sets itself as currentlySelected
    public override void Interact (Vector3 targetPosition){
        currentlySelected = gameObject;
    }

    public override void Initialize (Vector3 targetPosition){

    }

    public override void End (Vector3 targetPosition){

    }

    #endregion

	/// <summary>
	/// Changes to the next color collection in the list.
	/// </summary>
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

	/// <summary>
	/// Changes the object with the next object in the list
	/// </summary>
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

	/// <summary>
	/// Rotate the object by a certain angle multiplied with a rotation speed modifier to give more control.
	/// </summary>
	/// <param name="yAxisRotation">Y axis rotation.</param>
    public void Rotate(float yAxisRotation)
    {
		transform.Rotate(new Vector3(0, yAxisRotation * rotationSpeedModifier, 0));
        myIOScript.SampleRotation();
    }
}