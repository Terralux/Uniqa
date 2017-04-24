using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(fileName = "New BaseObject", order = 0, menuName = "Uniqa/Create Objects/BaseObject")]
public class BaseObject : CategoryElement {
	
	public GameObject prefab;

	[SerializeField]
	List<ColorCollection> myColors = new List<ColorCollection>();

	public ColorCollection GetColorsAt(int currentIndex)
    {
        return myColors[currentIndex];
    }

	public ColorCollection GetPreviousCollection(int currentIndex){
		return myColors[(currentIndex - 1 < 0 ? myColors.Count - 1 : currentIndex + 1)];
	}

	public ColorCollection GetNextCollection(int currentIndex){
		return myColors[(currentIndex + 1 > myColors.Count ? 0 : currentIndex + 1)];
	}

    public int GetLength()
    {
        return myColors.Count;
    }
}


[System.Serializable]
public class ColorCollection{
	public Color redColorOverride;
	public Color greenColorOverride;
	public Color blueColorOverride;
}