using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveRotatingElement : InteractiveObject {

	public enum Axis{	X,	Y,	Z	}
	public Axis rotationAxis = Axis.X;

	private Vector3 trackedPositionOne;
	private Vector3 trackedPositionTwo;

	private Transform pointer;

	void Start(){
		pointer = new GameObject (gameObject.name + "s pointer").transform;
		pointer.SetParent (transform);
		pointer.localPosition = Vector3.zero;
		pointer.localRotation = Quaternion.identity;
	}

	#region implemented abstract members of InteractiveObject

	public override void Initialize (Vector3 targetPosition) {
		Debug.Log ("I got initialized");

		switch (rotationAxis) {
		case Axis.X:
			break;
		case Axis.Y:
			break;
		case Axis.Z:
			break;
		}
	}

	public override void Interact (Vector3 targetPosition){
		Vector3 localSpaceTarget = transform.InverseTransformPoint (targetPosition);

		float angle;

		switch (rotationAxis) {
		case Axis.X:
			localSpaceTarget.x = 0;
			localSpaceTarget.Normalize ();

			angle = (Mathf.Acos(localSpaceTarget.y) * Mathf.Rad2Deg) % 360;
			transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.Euler (new Vector3 (0, 0, angle)), 0.8f);
			break;
		case Axis.Y:
			localSpaceTarget.y = 0;
			localSpaceTarget.Normalize ();

			angle = (Mathf.Acos(localSpaceTarget.z) * Mathf.Rad2Deg) % 360;
			transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.Euler (new Vector3 (0, 0, angle)), 0.8f);
			break;
		case Axis.Z:
			localSpaceTarget.z = 0;
			localSpaceTarget.Normalize ();

			angle = (Mathf.Acos(localSpaceTarget.x) * Mathf.Rad2Deg) % 360;
			transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.Euler (new Vector3 (0, 0, angle)), 0.8f);
			break;
		}
	}

	public override void End (Vector3 targetPosition){
		
	}
	#endregion
}