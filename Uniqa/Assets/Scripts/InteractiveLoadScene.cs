using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLoadScene : InteractiveObject {

	public int sceneBuildIndex;

	#region implemented abstract members of InteractiveObject

	public override void Initialize (Vector3 targetPosition)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneBuildIndex);
	}

	public override void Interact (Vector3 targetPosition)
	{

	}

	public override void End (Vector3 targetPosition)
	{

	}

	#endregion
}