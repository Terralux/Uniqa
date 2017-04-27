using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedObject : MonoBehaviour
{
	//This will be used for playing an animation on the currently selected object
    public static GameObject selectedObject;

	//Data container for the current status of the object
    [HideInInspector]
    public ObjectInfo currentObjectInfo;

	/// <summary>
	/// Initialize the BaseObject by instantiating a prefab based on the given parameters.
	/// </summary>
	/// <param name="category">Category.</param>
	/// <param name="objectIndex">Object index.</param>
	/// <param name="colorStartindex">Color startindex.</param>
	/// <param name="yRotation">Y rotation.</param>
    public void Initialize(Category category, int objectIndex, int colorStartindex, float yRotation)
    {
		//Used to ensure that we always only have 1 object spawned
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

		//get the BaseObject from the category
        BaseObject baseObject = category.GetElementAt(objectIndex) as BaseObject;

		//Instantiates the prefab from the BaseObject makes it a child of this object
        GameObject go = Instantiate(baseObject.prefab, transform.position, Quaternion.Euler(new Vector3(0, yRotation, 0)));
        go.transform.SetParent(transform);

		//Updates the objects color by accessing the shader and setting the override colors
		MeshRenderer mr = go.GetComponent<MeshRenderer> ();
		ColorCollection cc = baseObject.GetColorsAt (colorStartindex);

		mr.materials[0].SetColor("_RedColorChannel", cc.redColorOverride);
		mr.materials[0].SetColor("_GreenColorChannel", cc.greenColorOverride);
		mr.materials[0].SetColor("_BlueColorChannel", cc.blueColorOverride);

		//Adds the component making it possible to change the data of this object
        go.AddComponent<InteractiveChangableObject>();

		//stores the data in a data container
        currentObjectInfo = new ObjectInfo(category, objectIndex, colorStartindex, yRotation);
    }

	//Coroutine used to check for animation state --------------------------------------------------- NOT USED ANYWHERE YET!
    public IEnumerator WaitForNextAnimation()
    {
        yield return new WaitForSeconds(3f);

        if (selectedObject == gameObject)
        {
            Debug.Log("I am animating");
            StartCoroutine(WaitForNextAnimation());
        }
    }

	//Updates the rotation of the currentObjectInfo
    public void SampleRotation()
    {
        currentObjectInfo.yRotation = transform.GetChild(0).rotation.eulerAngles.y;
    }
}

//Data container for an instanced BaseObject
public class ObjectInfo
{
    public Category _category;
    public int currentObjectIndex;
    public int currentColorIndex;
    public float yRotation;

    public ObjectInfo(Category cat, int obj, int col, float rot)
    {
        _category = cat;
        currentObjectIndex = obj;
        currentColorIndex = col;
        yRotation = rot;
    }
}