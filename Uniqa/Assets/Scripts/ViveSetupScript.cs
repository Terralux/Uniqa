using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveSetupScript : MonoBehaviour {

	void Start (){
		Physics.gravity = Physics.gravity * 0.1f;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Terrain")) {
			Destroy (GetComponent<Rigidbody> ());
			Destroy (GetComponent<CapsuleCollider> ());
		}
	}
}
