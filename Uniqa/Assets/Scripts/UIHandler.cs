using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private InteractiveChangableObject io;
    public GameObject UIPrefab;

    public float offset = 2f;

    private bool wasPreviouslyActive = false;

    private GameObject instancedUIPrefab;

    private Transform player;

	//finds the player, instantiates the UI and then hides it
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        instancedUIPrefab = Instantiate(UIPrefab, Vector3.down * 10000, Quaternion.identity);
        instancedUIPrefab.SetActive(false);
    }

    //in update we determine whether we began, continued, ended or didn't at all interact
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

	//We enable the UI on began interaction
    void BeganInteraction()
    {
        instancedUIPrefab.SetActive(true);
    }

	//We track the players position and rotation in relation to the currently selected object
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

	//we disable the UI on ended interaction
    void EndedInteraction()
    {
        instancedUIPrefab.SetActive(false);
    }
}