using UnityEngine;
using System.Collections;

public class Grounded_2 : MonoBehaviour {


	private BoxCollider ParentCollider;
	private bool PlayerOnPlatform;
	private Player_Movement playerMovement;
	// Use this for initialization
	void Start () {
		PlayerOnPlatform = false;
		ParentCollider = transform.parent.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			PlayerOnPlatform = true;
			playerMovement = collider.transform.GetComponentInChildren<Player_Movement>();
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "Player")
		{
			PlayerOnPlatform = true;

			if (ParentCollider.enabled == false)
			{
				Debug.Log("Set to grounded");
				playerMovement.SetGrounded(false);
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Player")
		{
			PlayerOnPlatform = false;
			playerMovement.SetGrounded(false);
		}
	}
}
