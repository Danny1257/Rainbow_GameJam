using UnityEngine;
using System.Collections;

public class Disappear_TarPlatform : MonoBehaviour {

	public GameObject tarplatform1;
	public bool HasBombTargetTrigger;

	private BoxCollider tarplatform1_collider;
	private bool BombTriggered;

	// Use this for initialization
	void Start () {

		tarplatform1_collider = tarplatform1.GetComponentInChildren<BoxCollider> ();
		BombTriggered = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (HasBombTargetTrigger)
		{
			if (BombTriggered)
			{
				tarplatform1.GetComponentInChildren<Renderer>().enabled = true;
				tarplatform1_collider.enabled = true;
				tarplatform1.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = true;
				
			}
			else
			{
				tarplatform1.GetComponentInChildren<Renderer>().enabled = false;
				tarplatform1_collider.enabled = false;
			}
		}
	
	}

	public void SetBombTriggered(bool state)
	{
		BombTriggered = state;
	}
	
	public void ResetPlatform()
	{
		tarplatform1.GetComponentInChildren<Renderer>().enabled = false;
		tarplatform1_collider.enabled = false;
	}

	public bool GetPlatformColliderState(int platformNum)
	{
		if (platformNum == 1) {
			return tarplatform1_collider.enabled;
		} else {
			return true;
		}

	}
}
