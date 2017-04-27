using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLoadScene : InteractiveObject {

	public int sceneBuildIndex;
    public Color highlightColor;
    private Color defaultColor;

    void Awake()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
    }

    #region implemented abstract members of InteractiveObject

	public override void Initialize (Vector3 targetPosition)
	{
	    GetComponent<MeshRenderer>().material.SetColor("_Color", highlightColor);
	}

	public override void Interact (Vector3 targetPosition)
	{
	    //utilizes the SceneManager to load a specified scene upon initialization
	    UnityEngine.SceneManagement.SceneManager.LoadScene (sceneBuildIndex);
	}

	public override void End (Vector3 targetPosition)
	{
	    GetComponent<MeshRenderer>().material.SetColor("_Color", defaultColor);
	}

    #endregion
}