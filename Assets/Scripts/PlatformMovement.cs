using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour 
{
	public bool HasBombTargetTrigger;
	public float speed = 1;

	private int direction = 1;
	private bool BombTriggered;
	private Vector3 InitialPlatformPos;

	void Start()
	{
		BombTriggered = false;
		InitialPlatformPos = transform.position;
	}


	// Update is called once per frame
	void Update () 
	{
		if (HasBombTargetTrigger)
		{
			if (BombTriggered)
			{
				transform.Translate (Vector3.left * speed * direction * Time.deltaTime);
			}
		}
		else
		{
			transform.Translate (Vector3.left * speed * direction * Time.deltaTime);	
		}
	}

	public void SetBombTriggered(bool state)
	{
		BombTriggered = state;
	}

	public void ResetPlatform()
	{
		transform.position = InitialPlatformPos;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Target") 
		{
			if (direction == 1) 
			{
				direction = -1;
			}
			else 
			{
				direction = 1;
			}
		}
		if (other.tag == "Player")
		{
			other.transform.parent = transform;
		}
	}

	void OnTriggerExit(Collider other)
	{

		if(other.tag == "Player")
		{
			other.transform.parent = null;
		}
	}
}

