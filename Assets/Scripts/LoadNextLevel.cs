using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour 
{
	Player_Controller playerController;

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
			playerController = collider.transform.GetComponentInChildren<Player_Controller>();

			if (playerController.GetNumOfPickups() == 3)
			{
				Application.LoadLevel(1);
			}
		}
	}
}
