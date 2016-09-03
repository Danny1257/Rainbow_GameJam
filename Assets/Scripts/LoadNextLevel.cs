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
				if (Application.loadedLevelName == "Scene")
					Application.LoadLevel(1);
				else if (Application.loadedLevelName == "Level2")
					Application.LoadLevel(2);
				else if (Application.loadedLevelName == "Level3")
					Application.LoadLevel(3);
				else if (Application.loadedLevelName == "Level4")
					Application.LoadLevel(4);
			}
		}
	}
}
