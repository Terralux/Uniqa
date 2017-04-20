using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePopup : InteractiveObject {

	public GameObject objectToShow;
	//private PopupMaster pm;

	void Awake(){
		Hide ();
	}

	void Start(){
	//	pm = GameObject.FindGameObjectWithTag ("PopupMaster").GetComponent<PopupMaster> ();
	//	pm.OnSelectedAPopup += Hide;
	}

	public void Hide(){
		objectToShow.SetActive (false);
	}

	#region implemented abstract members of InteractiveObject

	public override void Interact (Vector3 targetPosition){
		
	}

	public override void Initialize (Vector3 targetPosition){
	//	if (pm.OnSelectedAPopup != null) {
	//		pm.OnSelectedAPopup ();
	//	}
	//	objectToShow.SetActive (true);
	}

	public override void End (Vector3 targetPosition){
		
	}

	#endregion
}