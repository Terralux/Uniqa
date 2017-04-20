using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaster : MonoBehaviour
{

    public Category myMainCategory;

    [SerializeField]
    List<TestInfo> myTestInfo = new List<TestInfo>();

    public GameObject instancedObjectPrefab;

	// Use this for initialization
	void Awake () {
	    for (int i = 0; i < myTestInfo.Count; i++)
	    {
	        Instantiate(instancedObjectPrefab)
	            .GetComponent<InstancedObject>()
	            .Initialize(myTestInfo[i].category, myTestInfo[i].objectIndex, myTestInfo[i].colorStartIndex,
	                myTestInfo[i].yRotation);
	    }
	}
}

[Serializable]
public class TestInfo
{
    public Category category;
    public int objectIndex;
    public int colorStartIndex;
    public float yRotation;
}