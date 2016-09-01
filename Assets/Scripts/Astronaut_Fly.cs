using UnityEngine;
using System.Collections;

public class Astronaut_Fly : MonoBehaviour 
{
	// public members
	public float FlyPower;
	public ParticleSystem my_particleSystem;
	public GameObject EmptyBar, PowerBar;

	//private members
	private bool FlyReady, RechargeTheTimer, Flying;
	private float RechargeTimer, FuelLeft, MaxBarScale, rate, fuelRate;
	private Player_Controller player_controller;


	// Use this for initialization
	void Start () 
	{
		FlyReady = true;
		RechargeTheTimer = false;
		RechargeTimer = 1.0f;
		FuelLeft = 100;
		Flying = false;
		my_particleSystem.Stop();
		MaxBarScale = PowerBar.transform.localScale.x;
		fuelRate = 100/2.5f;
		rate = MaxBarScale / 2.5f;
		player_controller = transform.GetComponentInChildren<Player_Controller> ();
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (FuelLeft < 100 && RechargeTheTimer == false && !Flying)
		{
			FuelLeft += (fuelRate * Time.deltaTime);

			//PowerBar.transform.localScale = new Vector3(PowerBar.transform.localScale.x + (rate * Time.deltaTime), PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);

			float XScale = (FuelLeft * MaxBarScale) / 100;
			
			PowerBar.transform.localScale = new Vector3 (XScale, PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);
			FlyReady = true;
		}

		if (player_controller.GetCurrentStatus() != Player_Controller.CharacterStatus.Astronaut) {
			StopParticles();
			RechargeTheTimer = false;
			Flying = false;
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
		EmptyBar.SetActive(true);
		PowerBar.SetActive(true);
		if (FlyReady)
		{
			if (!my_particleSystem.isPlaying)
				my_particleSystem.Play();
			Flying = true;

			FuelLeft -= (fuelRate * Time.deltaTime);

			//FuelLeft -= Time.deltaTime * (100/3);

			// Reduce fuel bar
			float XScale = (FuelLeft * MaxBarScale) / 100;
			
			PowerBar.transform.localScale = new Vector3 (XScale, PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);

			Rigidbody body = transform.GetComponentInChildren<Rigidbody>();

			Vector3 velocity = body.velocity;

			if (body.velocity.y < 0)
				velocity = new Vector3(velocity.x, body.velocity.y + FlyPower, velocity.z);
			else 
				velocity = new Vector3(velocity.x, FlyPower, velocity.z);

			body.velocity = velocity;


			if (FuelLeft <= 0)
			{
				FlyReady = false;
				my_particleSystem.Stop();
			}

			Debug.Log("Fuel Left = " + FuelLeft);
		}
	}

	public void StopParticles()
	{
		my_particleSystem.Stop ();
	}

	public void StartTimer()
	{
		RechargeTheTimer = true;
		Flying = false;
	}
}
