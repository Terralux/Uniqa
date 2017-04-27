using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Base object is any object that has to have a 3D mesh and a variety of attributes.
/// </summary>
[System.Serializable]
//This line ensures that you can create a new BaseObject from the project menu create -> Uniqa -> Create Objects
[CreateAssetMenu(fileName = "New BaseObject", order = 0, menuName = "Uniqa/Create Objects/BaseObject")]
public class BaseObject : CategoryElement {

	//This is the object which will be spawned
	public GameObject prefab;

	//The color pairs this object will contain
	[SerializeField]
	List<ColorCollection> myColors = new List<ColorCollection>();

	/// <summary>
	/// Gets the colors at currentIndex.
	/// </summary>
	/// <returns>The <see cref="ColorCollection"/>.</returns>
	/// <param name="currentIndex">Current index.</param>
	public ColorCollection GetColorsAt(int currentIndex)
    {
        return myColors[currentIndex];
    }

	/// <summary>
	/// Gets the previous collection.
	/// </summary>
	/// <returns>The previous collection.</returns>
	/// <param name="currentIndex">Current index.</param>
	public ColorCollection GetPreviousCollection(int currentIndex){
		return myColors[(currentIndex - 1 < 0 ? myColors.Count - 1 : currentIndex + 1)];
	}

	/// <summary>
	/// Gets the next collection.
	/// </summary>
	/// <returns>The next collection.</returns>
	/// <param name="currentIndex">Current index.</param>
	public ColorCollection GetNextCollection(int currentIndex){
		return myColors[(currentIndex + 1 > myColors.Count ? 0 : currentIndex + 1)];
	}

	/// <summary>
	/// Gets the length.
	/// </summary>
	/// <returns>The length.</returns>
    public int GetLength()
    {
        return myColors.Count;
    }
}

//A color collection is used to store a color scheme for a specific object
[System.Serializable]
public class ColorCollection{
	public Color redColorOverride;
	public Color greenColorOverride;
	public Color blueColorOverride;
}