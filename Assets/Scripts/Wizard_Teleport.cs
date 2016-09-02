using UnityEngine;
using System.Collections;

public class Wizard_Teleport : MonoBehaviour 
{
	public float dashDistance;
	public Light wizard_Light;
	public AudioSource TeleportAudio;

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

		//wizard_Light.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (StartEnableTimer)
		{
			wizard_Light.color = Color.red;
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
				wizard_Light.color = Color.blue;
			}
		}
	}

	public void Teleport()
	{
		TeleportAudio.Play ();
		transform.GetComponentInChildren<TrailRenderer>().startWidth = 0.8f;
		transform.GetComponentInChildren<TrailRenderer>().endWidth = 0.8f;

		if (CollideDetection() == false)
		{
			StartEnableTimer = true;
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
			CapsuleCollider playerCollider = transform.GetComponentInChildren<CapsuleCollider>();

			Vector3 newPlayerPos = new Vector3(transform.position.x + dashDistance, transform.position.y, transform.position.z);
			Debug.Log("player Y = " + transform.position.y);
			float playerRadiusX = playerCollider.radius;
			float playerRadiusY = playerCollider.height;
			Debug.Log("Player xRad = " + playerRadiusX);
			Debug.Log("Plpayer YRad = " + playerRadiusY);

			// check for x overlapp
			if ((newPlayerPos.x + playerRadiusX) >= hitCollider.bounds.min.x && (newPlayerPos.x - playerRadiusX) <= hitCollider.bounds.max.x)
			{
				Debug.Log("playerpos + rad = " + (newPlayerPos.y + playerRadiusY));
				Debug.Log("Collider bounds max = " + hitCollider.bounds.max.y);

				Debug.Log("playerpos - rad = " + (newPlayerPos.y - playerRadiusY));
				Debug.Log("Collider bounds min = " + hitCollider.bounds.min.y);

				if ((newPlayerPos.y + playerRadiusY) >= hitCollider.bounds.min.y && (newPlayerPos.y - playerRadiusY) <= hitCollider.bounds.max.y)
				{
					if (player_movement.FacingRight)
					{
						float XLeft = hitCollider.bounds.min.x;
						Vector3 newPos = new Vector3(XLeft - playerCollider.radius, transform.position.y, 0);
						Debug.Log("new pos = " + newPos);
						transform.position = newPos;
						Debug.Log("Left of object");
					}
					else
					{
						float XRight = hitCollider.bounds.max.x;
						Vector3 newPos = new Vector3(XRight + (playerCollider.radius), transform.position.y, 0);
						transform.Translate(newPos);
					}
					StartEnableTimer = true;
					return true;
				}
				else
					Debug.Log("NO OVERLAP!!");
			}
			else 
				Debug.Log("NO OVERLAPPPP!!");

		}

		return false;
	}

	public void SetPointLight(bool State)
	{
		//wizard_Light = transform.FindChild ("Point light").transform.GetComponent<Light> ();
		wizard_Light.transform.gameObject.SetActive (State);
		wizard_Light.enabled = State;

	}

	public bool GetTeleportReady()
	{
		return TeleportReady;
	}
}
