using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(fileName = "New BaseObject", order = 0, menuName = "Uniqa/Create Objects/BaseObject")]
public class BaseObject : CategoryElement {
	
	public GameObject prefab;

	[SerializeField]
	List<Color> myColors = new List<Color>();

    public Color GetColorAt(int currentIndex)
    {
        return myColors[currentIndex];
    }

    public Color GetPreviousElement(int currentIndex){
		return myColors[(currentIndex - 1 < 0 ? myColors.Count - 1 : currentIndex + 1)];
	}

	public Color GetNextElement(int currentIndex){
		return myColors[(currentIndex + 1 > myColors.Count ? 0 : currentIndex + 1)];
	}

    public int GetLength()
    {
        return myColors.Count;
    }
}