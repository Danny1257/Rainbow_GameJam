using UnityEngine;
using System.Collections;

public class Fall_Death : MonoBehaviour {

	private Player_Controller playerController;

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
			Debug.Log("player hit death trigger");
			playerController = collider.transform.GetComponentInChildren<Player_Controller>();
			playerController.Death();
		}
	}
}
