using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoFadeOut : MonoBehaviour {

	private Text myText;
	private Image myImage;

	public float fadeTime;
	static float collectiveFadeTime;
	private float currentFade;

	public int activeTimeBeforeFade = 3;

	private bool isFading = false;

	//upon awake, scans for an image or a text component on this object. then ensures synchronized fading and initiates the wait time
	void Awake () {
		myText = GetComponent<Text> ();
		myImage = GetComponent<Image> ();

		collectiveFadeTime = fadeTime;

		StartCoroutine (WaitForLogoShowTime ());
	}

	//used to determine the fade
	void Update(){
		if (isFading) {
			//we add deltaTime to ensure stability in case of frame loss
			currentFade += Time.deltaTime;

			//We adjust only the alpha value of the color
			if (myImage != null) {
				myImage.color = new Color (myImage.color.r, myImage.color.g, myImage.color.b, 1 - currentFade / collectiveFadeTime);
			}

			if(myText != null) {
				myText.color = new Color (myText.color.r, myText.color.g, myText.color.b, 1 - currentFade / collectiveFadeTime);
			}
		}
	}

	//waits for activetimebeforefade seconds and then triggers the fade
	IEnumerator WaitForLogoShowTime(){
		yield return new WaitForSeconds (activeTimeBeforeFade);
		isFading = true;
	}
}