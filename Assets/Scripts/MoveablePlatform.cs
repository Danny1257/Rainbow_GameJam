using UnityEngine;
using System.Collections;

public class MoveablePlatform : MonoBehaviour {

	public Vector3 FinalPos;
	public float Speed;

	private bool BombTriggered;
	private Vector3 StartPos;

	// Use this for initialization
	void Start () {
		BombTriggered = false;
		StartPos = transform.position;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (BombTriggered)
		{
			float PosX = transform.position.x + (Time.deltaTime * Speed);
			transform.position = new Vector3(PosX, transform.position.y, transform.position.z);

			if (transform.position.x >= FinalPos.x)
			{
				BombTriggered = false;
			}
		}
	}

	public void SetBombTrigger(bool state)
	{
		BombTriggered = state;
	}

	public void ResetPlatform()
	{
		transform.position = StartPos;
		BombTriggered = false;
	}
}
