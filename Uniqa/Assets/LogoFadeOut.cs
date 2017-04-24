using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoFadeOut : MonoBehaviour {

	private Text myText;
	private Image myImage;

	public float fadeTime;
	private static collectiveFadeTime;

	void Awake () {
		myText = GetComponent<Text> ();
		myImage = GetComponent<Image> ();

		StartCoroutine()
	}
	
	void Update () {
		
	}
}