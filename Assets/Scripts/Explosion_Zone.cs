using UnityEngine;
using System.Collections;

public class Explosion_Zone : MonoBehaviour {

	private bool BombInTargetZone;
	private GameObject LastHitTarget;

	// Use this for initialization
	void Start () 
	{
		BombInTargetZone = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public bool GetBombInTargetZone()
	{
		return BombInTargetZone;
	}

	public GameObject GetLastHitTarget()
	{
		return LastHitTarget;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "BombTarget")
		{
			Debug.Log ("Bomb hit the target!");
			LastHitTarget = collider.gameObject;
			BombInTargetZone = true;
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "BombTarget")
		{
			BombInTargetZone = true;
			LastHitTarget = collider.gameObject;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "BombTarget")
			BombInTargetZone = false;
	}

}
