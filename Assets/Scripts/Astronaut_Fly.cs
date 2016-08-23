﻿using UnityEngine;
using System.Collections;

public class Astronaut_Fly : MonoBehaviour 
{
	// public members
	public float FlyPower;

	//private members
	private bool FlyReady, RechargeTheTimer, Flying;
	private float RechargeTimer, FuelLeft;

	// Use this for initialization
	void Start () 
	{
		FlyReady = true;
		RechargeTheTimer = false;
		RechargeTimer = 1.0f;
		FuelLeft = 100;
		Flying = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (FuelLeft < 100 && RechargeTheTimer == false && !Flying)
		{
			FuelLeft += Time.deltaTime * (100/3);
			FlyReady = true;
		}


		if (RechargeTheTimer)
		{
			RechargeTimer -= Time.deltaTime;
			if (RechargeTimer <= 0)
			{
				RechargeTheTimer = false;
				FlyReady = true;
				RechargeTimer = 1.0f;
			}
		}	
	}

	public void Fly()
	{
		if (FlyReady)
		{
			Flying = true;
			FuelLeft -= Time.deltaTime * (100/3);

			Rigidbody body = transform.GetComponentInChildren<Rigidbody>();

			Vector3 velocity = body.velocity;

			if (body.velocity.y < 0)
				velocity = new Vector3(velocity.x, body.velocity.y + FlyPower, velocity.z);
			else 
				velocity = new Vector3(velocity.x, FlyPower, velocity.z);

			body.velocity = velocity;


			if (FuelLeft <= 0)
				FlyReady = false;

			Debug.Log("Fuel Left = " + FuelLeft);
		}
	}

	public void StartTimer()
	{
		RechargeTheTimer = true;
		Flying = false;
	}
}