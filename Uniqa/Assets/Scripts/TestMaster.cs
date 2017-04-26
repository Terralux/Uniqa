using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaster : MonoBehaviour
{
	//This is the top category object
    public Category myMainCategory;

	void Awake ()
	{
	    //locates all the objects that needs to spawn
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Respawn");

		//Foreach of these objects, generate some data and use it to initialize the objects prefab
	    foreach (GameObject go in gos)
	    {
			ObjectData od = GenerateData();
			go.GetComponent<InstancedObject> ().Initialize (myMainCategory.GetElementAt (od.categoryIndex) as Category, od.objectIndex, od.colorIndex, od.yRotation);
	    }
	}

	//Used to generate random category, object, color and rotation data
	ObjectData GenerateData(){
		int categoryID = Random.Range (0, 0);//myMainCategory.GetLength ());
		int objectID = Random.Range (0, (myMainCategory.GetElementAt (categoryID) as Category).GetLength ());
		int colorID = Random.Range (0, ((myMainCategory.GetElementAt (categoryID) as Category).GetElementAt(objectID) as BaseObject).GetLength());
		float rotation = Random.Range (0f, 360f);

		return new ObjectData (categoryID, objectID, colorID, rotation);
	}
}

public class ObjectData{
	public int categoryIndex;
	public int objectIndex;
	public int colorIndex;
	public float yRotation;
	
	public ObjectData(int ca, int o, int co, float rot){
		categoryIndex = ca;
		objectIndex = o;
		colorIndex = co;
		yRotation = rot;
	}
}