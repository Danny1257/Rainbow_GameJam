using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour 
{
	// Public Members
	public float PlayerSpeed, JumpPower;


	// Private Memebers
	private Vector3 PlayerVelocity;
	private bool Grounded, FacingRight, MovingLeft, MovingRight;

	// Use this for initialization
	void Start () 
	{
		PlayerVelocity = new Vector3(0,0,0);
		Grounded = false;
		FacingRight = true;
		MovingLeft = false;
		MovingRight = false;
	}	
	
	// Update is called once per frame
	void Update () 
	{
		CheckForInput();			// Check for player input
		UpdatePosition();			// Update the players position
	}

	void CheckForInput()
	{
		if (Input.GetKeyDown(KeyCode.A))		// Player moving left
		{
			FacingRight = false;
			MovingLeft = true;
		}
		else if (Input.GetKey(KeyCode.D))			// Player moving right
		{
			FacingRight = true;
			MovingRight = true;
		}

		if (Input.GetKeyUp(KeyCode.A))
		{
			MovingLeft = false;
		}

		if (Input.GetKeyUp(KeyCode.D))
		{
			MovingRight = false;
		}

		// Check for Jump input
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Grounded)
			{
				Jump();			
			}
		}
	}

	void UpdatePosition()
	{
		Rigidbody player_Rigidbody = this.transform.GetComponentInChildren<Rigidbody>();						// Initialise the player RigidBody

		Vector3 velocity = new Vector3(0, player_Rigidbody.velocity.y, player_Rigidbody.velocity.z);			// Initialise the player velocity

		if (MovingLeft)
		{
			velocity = new Vector3(-PlayerSpeed, player_Rigidbody.velocity.y, player_Rigidbody.velocity.z);		// Set the player moving left velocity
		}
		else if (MovingRight)
		{
			velocity = new Vector3(PlayerSpeed, player_Rigidbody.velocity.y, player_Rigidbody.velocity.z);		// Set the player moving right velocity
		}
		player_Rigidbody.velocity = velocity;			// Set the final player velocity



	}

	void Jump()
	{

		Rigidbody player_Rigidbody = this.transform.GetComponentInChildren<Rigidbody>();						// Initialise the player RigidBody

		Vector3 velocity = new Vector3(player_Rigidbody.velocity.x, JumpPower, player_Rigidbody.velocity.z);

		player_Rigidbody.velocity = velocity;
		Grounded = false;

		Debug.Log("Jump");
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Platform")
		{
			Grounded = true;
		}
	}



}
