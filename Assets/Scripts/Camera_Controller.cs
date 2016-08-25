using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour 
{
	private Transform player;

	private float offsetX, offsetY, MinY;
	// Use this for initialization
	void Start () 
	{
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");

		if (player_go == null)
		{
			Debug.LogError("Couldn't find an object with the tag 'Player'!");
			return;
		}
		player = player_go.transform;

		offsetX = transform.position.x - player.position.x;
		offsetY = transform.position.y - player.position.y;

		MinY = -3.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 PlayerScreenPos;

		if (player != null)
		{
			Vector3 pos = transform.position;
			pos.x = player.position.x + offsetX;
			pos.y = player.position.y + offsetY;
			transform.position = pos;
		}
		else
			Debug.LogError("Player is null");
	}
}
