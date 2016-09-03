using UnityEngine;
using System.Collections;

public class Disappear_TarPlatform : MonoBehaviour {

	public GameObject tarplatform1_Mesh;
	public bool HasBombTargetTrigger;

	private BoxCollider tarplatform1_collider, GroundedCollider;
	private bool BombTriggered;

	// Use this for initialization
	void Start () {

		tarplatform1_collider = transform.GetComponent<BoxCollider> ();
		GroundedCollider = transform.GetChild (0).transform.GetComponent<BoxCollider> ();
		BombTriggered = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (HasBombTargetTrigger)
		{
			if (BombTriggered)
			{
				tarplatform1_Mesh.SetActive(true);
				tarplatform1_collider.enabled = true;
				GroundedCollider.enabled = true;
								
			}
			else
			{
				tarplatform1_Mesh.SetActive(false);

				tarplatform1_collider.enabled = false;
				GroundedCollider.enabled = false;
			}
		}
	
	}

	public void SetBombTriggered(bool state)
	{
		BombTriggered = state;
	}
	
	public void ResetPlatform()
	{
		tarplatform1_Mesh.SetActive (false);
		tarplatform1_collider.enabled = false;
		GroundedCollider.enabled = false;
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
