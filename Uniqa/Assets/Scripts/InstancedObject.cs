using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedObject : MonoBehaviour
{
    public static GameObject selectedObject;

    [HideInInspector]
    public ObjectInfo currentObjectInfo;

    public void Initialize(Category category, int objectIndex, int colorStartindex, float yRotation)
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        BaseObject baseObject = category.GetElementAt(objectIndex) as BaseObject;

        GameObject go = Instantiate(baseObject.prefab, transform.position, Quaternion.Euler(new Vector3(0, yRotation, 0)));
        go.transform.SetParent(transform);

        go.GetComponent<MeshRenderer>().material.color = baseObject.GetColorAt(colorStartindex);
        go.AddComponent<InteractiveChangableObject>();

        currentObjectInfo = new ObjectInfo(category, objectIndex, colorStartindex, yRotation);
    }

    public IEnumerator WaitForNextAnimation()
    {
        yield return new WaitForSeconds(3f);

        if (selectedObject == gameObject)
        {
            Debug.Log("I am animating");
            StartCoroutine(WaitForNextAnimation());
        }
    }

    public void SampleRotation()
    {
        currentObjectInfo.yRotation = transform.GetChild(0).rotation.eulerAngles.y;
    }
}

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