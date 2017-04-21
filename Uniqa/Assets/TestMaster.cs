using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaster : MonoBehaviour
{

    public Category myMainCategory;

	// Use this for initialization
	void Awake ()
	{
	    Category targetCategory = (myMainCategory.GetElementAt(0) as Category) as Category;
	    BaseObject targetObject = targetCategory.GetElementAt(0) as BaseObject;

	    GameObject[] gos = GameObject.FindGameObjectsWithTag("Respawn");

	    foreach (GameObject go in gos)
	    {
	        go.GetComponent<InstancedObject>().Initialize(targetCategory, 0, 0, Random.Range(0, 360));
	    }
	}
}