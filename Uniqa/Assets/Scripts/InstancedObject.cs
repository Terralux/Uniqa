using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(Category category, int objectIndex, int colorStartindex, float yRotation)
    {
        BaseObject baseObject = category.GetElementAt(objectIndex) as BaseObject;

        GameObject go = Instantiate(baseObject.prefab, transform.position, Quaternion.Euler(new Vector3(0, yRotation, 0)));
        go.transform.SetParent(transform);

        go.GetComponent<MeshRenderer>().material.color = baseObject.GetColorAt(colorStartindex);
    }
}