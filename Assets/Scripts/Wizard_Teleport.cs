using UnityEngine;
using System.Collections;

public class Wizard_Teleport : MonoBehaviour 
{
	public float dashDistance;

	private Player_Movement player_movement;
	private float enableTimer;
	private bool StartEnableTimer;
	// Use this for initialization
	void Start ()
	{
		player_movement = transform.GetComponentInChildren<Player_Movement>();
		enableTimer = 0.5f;
		StartEnableTimer = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (StartEnableTimer)
		{
			enableTimer -= Time.deltaTime;

			if (enableTimer <= 0)
			{
				enableTimer = 0.5f;
				transform.GetComponentInChildren<TrailRenderer>().endWidth = 0;
				transform.GetComponentInChildren<TrailRenderer>().startWidth = 0;
				StartEnableTimer = false;

			}

		}
	}

	public void Teleport()
	{
		transform.GetComponentInChildren<TrailRenderer>().startWidth = 0.8f;
		transform.GetComponentInChildren<TrailRenderer>().endWidth = 0.8f;

		StartEnableTimer = true;
		Rigidbody body = this.transform.GetComponentInChildren<Rigidbody>();
		Vector3 velocity;
		if (player_movement.FacingRight)
		{
			velocity = new Vector3(dashDistance, 0, 0);
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + dashDistance, Camera.main.transform.position.y, Camera.main.transform.position.z);
			transform.Translate(velocity);


		}
		else
		{
			velocity = new Vector3(-dashDistance, 0, 0);
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - dashDistance, Camera.main.transform.position.y, Camera.main.transform.position.z);
			transform.Translate(velocity);
		}
	}
}
