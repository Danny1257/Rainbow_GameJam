using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour 
{
	public Transform player;

	private AudioSource backgroundMusic;
	private float offsetX, offsetY;
	// Use this for initialization
	void Start () 
	{
		offsetX = transform.position.x - player.position.x;
		offsetY = transform.position.y - player.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log("Player name = " + player.name);
		Debug.Log("Player pos = " + player.transform.position);
		if (player != null)
		{
			Vector3 pos = transform.position;
			pos.x = player.position.x + offsetX;
			pos.y = player.position.y + offsetY;

			if (pos.y < -3.5f)
			{
				pos.y = -3.5f;
			}

			transform.position = pos;
		}
		else
			Debug.LogError("Player is null");
	}
}
