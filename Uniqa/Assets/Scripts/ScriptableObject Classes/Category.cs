using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(fileName = "New Category", order = 1, menuName = "Uniqa/Create Objects/Category")]
public class Category : CategoryElement {
	[SerializeField]
	List<CategoryElement> categoryElements = new List<CategoryElement>();

    public CategoryElement GetElementAt(int currentIndex)
    {
        return categoryElements[currentIndex];
    }

    public CategoryElement GetPreviousElement(int currentIndex){
		return categoryElements[(currentIndex - 1 < 0 ? categoryElements.Count - 1 : currentIndex + 1)];
	}

	public CategoryElement GetNextElement(int currentIndex) {
		return categoryElements[(currentIndex + 1 > categoryElements.Count? 0 : currentIndex + 1)];
	}
}