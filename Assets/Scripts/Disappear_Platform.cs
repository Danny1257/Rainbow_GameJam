using UnityEngine;
using System.Collections;

public class Disappear_Platform : MonoBehaviour {

	public GameObject platform1;
	public GameObject platform2;
	public GameObject platform3;


	private float timer = 5;
	private float timer2 = 8;
	private BoxCollider platform1_collider, platform2_collider, platform3_collider;


	void Start()
	{
		if (platform1 != null) {
			platform1_collider = platform1.GetComponentInChildren<BoxCollider> ();
		}

		if (platform2 != null) {
			platform2_collider = platform2.GetComponentInChildren<BoxCollider> ();
		}
		if (platform3!= null) {
			platform3_collider = platform3.GetComponentInChildren<BoxCollider> ();
		}
	
	}

	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;
		timer2 -= Time.deltaTime;

		if (platform1 != null) {
			if (timer <= 2.5) {
				platform1.GetComponentInChildren<Renderer> ().enabled = false;
				platform1_collider.enabled = false;

				// Check if player is still in the grounded sensors trigger box
			}

			if (timer <= 0) {
				platform1.GetComponentInChildren<Renderer> ().enabled = true;
				platform1_collider.enabled = true;

				platform1.transform.GetChild (0).GetComponentInChildren<BoxCollider> ().enabled = true;

			}
		}

		if (platform2 != null) {
			if (timer <= 3) {
				platform2.GetComponentInChildren<Renderer> ().enabled = true;
				platform2_collider.enabled = true;

				platform2.transform.GetChild (0).GetComponentInChildren<BoxCollider> ().enabled = true;
			}

			if (timer <= 0.5) {
				platform2.GetComponentInChildren<Renderer> ().enabled = false;
				platform2_collider.enabled = false;
			}	
		}

		if (platform3 != null) {
			if (timer2 <= 8) {
				platform3.GetComponentInChildren<Renderer> ().enabled = true;
				platform3_collider.enabled = true;
				
				platform3.transform.GetChild (0).GetComponentInChildren<BoxCollider> ().enabled = true;
			}
			
			if (timer2 <= 2) {
				platform3.GetComponentInChildren<Renderer> ().enabled = false;
				platform3_collider.enabled = false;
			}	
		}

		if (timer <= 0) {
			timer = 5;
		}
		if (timer2 <= 0) {
			timer2 = 8;
		}
	}
	

	public bool GetPlatformColliderState(int platformNum)
	{
		if (platformNum == 1) {
			return platform1_collider.enabled;
		} 
		else if (platformNum == 2){
			return platform2_collider.enabled;
		}
		else{
			return platform3_collider.enabled;
		}

	}
}
