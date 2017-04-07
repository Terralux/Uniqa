using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetSelection : MonoBehaviour {

	public float waitTime = 0.1f;
	public float speed = 10f;

	private LookTarget lt;
	private LookTarget prevLt;

	private bool isInFocus;
	private float passedTime;
	private Image uiTimer;

	// Use this for initialization
	void Awake () {
		uiTimer = GameObject.FindGameObjectWithTag ("UI Timer").GetComponent<Image> ();
		StartCoroutine (WaitForNextRaycast ());
	}

	IEnumerator WaitForNextRaycast(){
		yield return new WaitForSeconds (waitTime);
		RaycastHit hit = new RaycastHit ();
		Ray ray = new Ray (transform.position, transform.forward);

		isInFocus = false;

		if (Physics.Raycast (ray, out hit, 100f)) {
			if (hit.collider != null) {
				prevLt = lt;
				lt = hit.collider.gameObject.GetComponent<LookTarget> ();

				if (lt != null) {
					if ((lt as WobbleTarget).isActiveAndEnabled) {
						isInFocus = true;
						lt.Focus (isInFocus);

						if (lt != prevLt) {
							passedTime = 0f;
							if (prevLt != null) {
								prevLt.Focus (false);
							}
						}
					}
				}
			}
		}

		StartCoroutine (WaitForNextRaycast ());
	}

	void Update () {
		if (isInFocus) {
			passedTime += Time.deltaTime;
			uiTimer.fillAmount = passedTime / 3f;

			if (passedTime >= 3) {
				isInFocus = false;
				lt.Action ();
				uiTimer.fillAmount = 0f;
				this.enabled = false;
			}
		} else {
			if (lt != null) {
				lt.Focus (false);
			}
			if (prevLt != null) {
				prevLt.Focus (false);
			}
			Reset ();
		}
	}

	public void Reset(){
		passedTime = 0f;
		isInFocus = false;
		uiTimer.fillAmount = passedTime / 3f;
	}

}
