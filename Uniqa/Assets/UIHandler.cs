using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private InteractiveChangableObject io;
    public GameObject UIPrefab;

    public float offset = 1f;

    private bool wasPreviouslyActive = false;

    private GameObject instancedUIPrefab;

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        instancedUIPrefab = Instantiate(UIPrefab, Vector3.down * 10000, Quaternion.identity);
        instancedUIPrefab.SetActive(false);
    }

    // Update is called once per frame
	void Update () {
	    if (InteractiveChangableObject.currentlySelected != null)
	    {
	        if (!wasPreviouslyActive)
	        {
	            BeganInteraction();
	            wasPreviouslyActive = true;
	        }
	        else
	        {
	            ContinousInteraction();
	        }
	    }
	    else
	    {
	        if (wasPreviouslyActive)
	        {
	            EndedInteraction();
	            wasPreviouslyActive = false;
	        }
	    }
	}

    void BeganInteraction()
    {
        instancedUIPrefab.SetActive(true);
    }

    void ContinousInteraction()
    {
        instancedUIPrefab.transform.position = player.transform.position +
                                               (InteractiveChangableObject.currentlySelected.transform.position -
                                                player.transform.position).normalized *
                                               offset;
        instancedUIPrefab.transform.rotation = Quaternion.LookRotation(
        (InteractiveChangableObject.currentlySelected.transform.position -
         player.transform.position).normalized, Vector3.up);
    }

    void EndedInteraction()
    {
        instancedUIPrefab.SetActive(false);
    }
}