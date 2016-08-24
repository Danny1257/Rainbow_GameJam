using UnityEngine;
using System.Collections;

public class Wizard_Teleport : MonoBehaviour 
{
	public float dashDistance;

	private Player_Movement player_movement;
	private float enableTimer, RechargeTimer;
	private bool StartEnableTimer, TeleportReady, StartRechargeTimer;
	// Use this for initialization
	void Start ()
	{
		player_movement = transform.GetComponentInChildren<Player_Movement>();
		enableTimer = 0.5f;
		RechargeTimer = 2.0f;
		StartEnableTimer = false;
		StartRechargeTimer = false;
		TeleportReady = true;
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

		if (StartRechargeTimer)
		{
			RechargeTimer -= Time.deltaTime;

			if (RechargeTimer <= 0)
			{
				RechargeTimer = 2.0f;
				StartRechargeTimer = false;
				TeleportReady = true;
			}
		}


	}

	public void Teleport()
	{
		transform.GetComponentInChildren<TrailRenderer>().startWidth = 0.8f;
		transform.GetComponentInChildren<TrailRenderer>().endWidth = 0.8f;

		if (CollideDetection() == false)
		{
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

		TeleportReady = false;
		StartRechargeTimer = true;

	}

	private bool CollideDetection()
	{
		Vector3 facingDirection;

		if (player_movement.FacingRight == true)
			facingDirection = new Vector3(1, 0, 0);
		else 
			facingDirection = new Vector3(-1, 0, 0);

		Ray playerRay = new Ray(transform.position, facingDirection);
		RaycastHit hit;

		if (Physics.Raycast(playerRay, out hit, dashDistance))
		{
			// Box collision

			BoxCollider hitCollider = hit.transform.GetComponentInChildren<BoxCollider>();
			BoxCollider playerCollider = transform.GetComponentInChildren<BoxCollider>();

			Vector3 newPlayerPos = new Vector3(transform.position.x + dashDistance, transform.position.y, transform.position.z);
			float playerRadiusX = playerCollider.size.x;
			float playerRadiusY = playerCollider.size.y;

			// check for x overlapp
			if ((newPlayerPos.x + playerRadiusX) <= hitCollider.bounds.max.x && (newPlayerPos.x - playerRadiusX) >= hitCollider.bounds.min.x)
			{
				if ((newPlayerPos.y + playerRadiusY) <= hitCollider.bounds.max.y && (newPlayerPos.y - playerRadiusY) >= hitCollider.bounds.min.y)
				{
					if (player_movement.FacingRight)
					{
						float XLeft = hitCollider.bounds.min.x;
						Vector3 newPos = new Vector3(XLeft - playerCollider.size.x, transform.position.y, 0);
						Debug.Log("new pos = " + newPos);
						transform.position = newPos;
						Debug.Log("Left of object");
					}
					else
					{
						float XRight = hitCollider.bounds.max.x;
						Vector3 newPos = new Vector3(XRight + (playerCollider.size.x), transform.position.y, 0);
						transform.Translate(newPos);
					}
					return true;
				}
				else
					Debug.Log("NO OVERLAP!!");
			}
			else 
				Debug.Log("NO OVERLAPPPP!!");




			Debug.Log("Hit object = " + hit.transform.name);

		}

		return false;
	}

	public bool GetTeleportReady()
	{
		return TeleportReady;
	}
}
