﻿using UnityEngine;
using System.Collections;

public class Grounded : MonoBehaviour {

	public int platform_number;

	public Disappear_Platform parentPlatformScript;
	private GameObject Player;
	private Player_Movement playerMovement;
	private bool playerOnPlatform;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (parentPlatformScript.GetPlatformColliderState(platform_number) == false)
		{
			if (!playerOnPlatform)
			{
				transform.GetComponentInChildren<BoxCollider>().enabled = false;
			}
			else
			{
				transform.GetComponentInChildren<BoxCollider>().enabled = false;
				playerMovement.SetGrounded(false);
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			playerOnPlatform = true;
			Player = collider.gameObject;
			playerMovement = Player.transform.GetComponentInChildren<Player_Movement>();

			playerMovement.SetGrounded(true);
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "Player")
		{
			playerOnPlatform = true;
			if (parentPlatformScript.GetPlatformColliderState(platform_number) == false)
			{
				Debug.Log("Set grounded to false");
				transform.GetComponentInChildren<BoxCollider>().enabled = false;
				playerMovement.SetGrounded(false);
				playerOnPlatform = false;

			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Player")
		{
			playerOnPlatform = false;
			if (playerMovement != null)
			{
				playerMovement.SetGrounded(false);
			}
		}
	}
}
