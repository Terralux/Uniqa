using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Category is a list of Categories and/or BaseObjects
/// </summary>
[System.Serializable]
//can be created from the project windows create button by selecting -> Uniqa -> Create Objects -> Category
[CreateAssetMenu(fileName = "New Category", order = 1, menuName = "Uniqa/Create Objects/Category")]
public class Category : CategoryElement {
	//This is the list you'll see in the inspector in Unity
	[SerializeField]
	List<CategoryElement> categoryElements = new List<CategoryElement>();

	/// <summary>
	/// Gets the element at currentIndex.
	/// </summary>
	/// <returns>The <see cref="CategoryElement"/>.</returns>
	/// <param name="currentIndex">Current index.</param>
    public CategoryElement GetElementAt(int currentIndex)
    {
        return categoryElements[currentIndex];
    }

	/// <summary>
	/// Gets the previous element.
	/// </summary>
	/// <returns>The previous element.</returns>
	/// <param name="currentIndex">Current index.</param>
    public CategoryElement GetPreviousElement(int currentIndex){
		return categoryElements[(currentIndex - 1 < 0 ? categoryElements.Count - 1 : currentIndex + 1)];
	}

	/// <summary>
	/// Gets the next element.
	/// </summary>
	/// <returns>The next element.</returns>
	/// <param name="currentIndex">Current index.</param>
	public CategoryElement GetNextElement(int currentIndex) {
		return categoryElements[(currentIndex + 1 > categoryElements.Count? 0 : currentIndex + 1)];
	}

	/// <summary>
	/// Gets the length.
	/// </summary>
	/// <returns>The length.</returns>
    public int GetLength()
    {
        return categoryElements.Count;
    }
}