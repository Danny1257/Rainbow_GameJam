using UnityEngine;
using System.Collections;

public class Disappear_2_Platform : MonoBehaviour 
{
	public GameObject PlatformMesh;

	private BoxCollider MainCollider;
	private BoxCollider GroundedCollider;


	private float timer = 8.0f;
	// Use this for initialization
	void Start () 
	{
		MainCollider = transform.GetChild (0).GetComponent<BoxCollider> ();
		GroundedCollider = MainCollider.transform.GetChild (0).GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;

		if (timer <= 8)
		{
			MainCollider.enabled = true;
			GroundedCollider.enabled = true;

			// enable renderer
			PlatformMesh.SetActive(true);
		}

		if (timer <= 2) 
		{
			MainCollider.enabled = false;
			GroundedCollider.enabled = false;

			// disbable renderer
			PlatformMesh.SetActive(false);
		}

		if (timer <= 0)
			timer = 8;
	}

	public bool GetPlatformState()
	{
		return MainCollider.enabled;
	}
}
