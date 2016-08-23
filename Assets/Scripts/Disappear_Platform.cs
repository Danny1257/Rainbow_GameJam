using UnityEngine;
using System.Collections;

public class Disappear_Platform : MonoBehaviour {

	public GameObject platform1;
	public GameObject platform2;

	private float timer = 5;
	private BoxCollider platform1_collider, platform2_collider;
	private GameObject Grounded_Sensor1, Grounded_Sensor2;

	void Start()
	{
		platform1_collider = platform1.GetComponentInChildren<BoxCollider>();
		platform2_collider = platform2.GetComponentInChildren<BoxCollider>();

		Grounded_Sensor1 = platform1.transform.GetChild(0).gameObject;
		Grounded_Sensor2 = platform2.transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;

		if (timer <= 2.5) 
		{
			platform1.GetComponentInChildren<Renderer>().enabled = false;
			platform1_collider.enabled = false;

			// Check if player is still in the grounded sensors trigger box

		}

		if (timer <= 0) 
		{
			platform1.GetComponentInChildren<Renderer>().enabled = true;
			platform1_collider.enabled = true;
			timer = 5;
		}

		if (timer <= 3)
		{
			platform2.GetComponentInChildren<Renderer>().enabled = true;
		}

		if (timer <= 0.5) 
		{
			platform2.GetComponentInChildren<Renderer>().enabled = false;
		}	
	}

	public bool GetPlatformColliderState(int platformNum)
	{
		if (platformNum == 1)
		{
			return platform1_collider.enabled;
		}
		else 
			return platform2_collider.enabled;
	}
}
