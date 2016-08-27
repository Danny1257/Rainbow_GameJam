using UnityEngine;
using System.Collections;

public class Explosion_Zone : MonoBehaviour {

	private bool BombInTargetZone;

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

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "BombTarget")
		{
			Debug.Log ("Bomb hit the target!");
			BombInTargetZone = true;
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "BombTarget")
			BombInTargetZone = true;
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "BombTarget")
			BombInTargetZone = false;
	}

}
