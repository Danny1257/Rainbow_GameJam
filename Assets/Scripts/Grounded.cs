using UnityEngine;
using System.Collections;

public class Grounded : MonoBehaviour {

	public int platform_number;

	public Disappear_Platform parentPlatformScript;
	private GameObject Player;
	private Player_Movement playerMovement;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			Player = collider.gameObject;
			playerMovement = Player.transform.GetComponentInChildren<Player_Movement>();

			playerMovement.SetGrounded(true);
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "Player")
		{
			if (parentPlatformScript.GetPlatformColliderState(platform_number) == false)
			{
				playerMovement.SetGrounded(false);
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Player")
		{
			if (playerMovement != null)
			{
				playerMovement.SetGrounded(false);
			}
		}
	}
}
