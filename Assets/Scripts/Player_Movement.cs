using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour 
{
	// Public Members
	public float PlayerSpeed, JumpPower;
	public bool FacingRight;


	// Private Memebers
	private Vector3 PlayerVelocity;
	private bool Grounded, MovingLeft, MovingRight;
	private float InitialXScale;

	// Use this for initialization
	void Start () 
	{
		PlayerVelocity = new Vector3(0,0,0);
		Grounded = false;
		FacingRight = true;
		MovingLeft = false;
		MovingRight = false;
		InitialXScale = transform.localScale.x;
	}	
	
	// Update is called once per frame
	void Update () 
	{
		CheckForInput();			// Check for player input
		UpdatePosition();			// Update the players position

		if (MovingRight)
			FacingRight = true;
		else if (MovingLeft)
			FacingRight = false;


		if (FacingRight)
		{
			transform.localScale = new Vector3(InitialXScale, transform.localScale.y, transform.localScale.z);
		}
		else
		{
			transform.localScale = new Vector3(InitialXScale * -1, transform.localScale.y, transform.localScale.z);
		}

	}

	void CheckForInput()
	{
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			MovingLeft = false;
			MovingRight = false;
		}
		else
		{
			if (Input.GetKey(KeyCode.A))
			{
				MovingLeft = true;
			}

			if (Input.GetKey(KeyCode.D))
			{
				MovingRight = true;
			}
		}


		if (Input.GetKeyDown(KeyCode.A))		// Player moving left
		{
			FacingRight = false;
			//MovingLeft = true;
		}

		if (Input.GetKeyDown(KeyCode.D))			// Player moving right
		{
			FacingRight = true;
			//MovingRight = true;
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
	}

	public bool GetGrounded()
	{
		return Grounded;
	}

	public void SetGrounded(bool State)
	{
		Grounded = State;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.transform.tag == "Platform")
		{
			Grounded = true;
			//Debug.Log("Grounded");
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.transform.tag == "Platform")
		{
			Grounded = true;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.transform.tag == "Platform")
		{
			Grounded = false;
			//Debug.Log("Not Grounded");
		}
	}
}
