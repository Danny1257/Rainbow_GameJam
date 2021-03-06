﻿using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour 
{
	public int PickUpNumber;

	private Player_Controller player_controller;
	private float speed, maxY, minY;
	private Vector3 velocity;



	// Use this for initialization
	void Start () 
	{
		speed = 0.02f;
		minY = transform.position.y - 0.5f;
		maxY = transform.position.y + 0.5f;
		velocity = new Vector3(0, speed, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += velocity;

		// create floating motion
		if (velocity.y > 0)
		{
			if (transform.position.y >= maxY)
			{
				velocity = new Vector3(0, -speed, 0);
			}
		}
		else
		{
			if (transform.position.y <= minY)
			{
				velocity = new Vector3(0, speed, 0);
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Player")
		{
			player_controller = collision.transform.GetComponentInChildren<Player_Controller>();
			player_controller.Pickup(PickUpNumber, transform.position);

			Destroy(this.gameObject);
		}
	}
}
