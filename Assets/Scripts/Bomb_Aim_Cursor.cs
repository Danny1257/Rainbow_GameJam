using UnityEngine;
using System.Collections;

public class Bomb_Aim_Cursor : MonoBehaviour 
{
	public GameObject BombAim_Cursor;

	private Player_Controller playerController;


	// Use this for initialization
	void Start () 
	{
		playerController = transform.GetComponentInChildren<Player_Controller> ();
		BombAim_Cursor.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerController.GetCurrentStatus () == Player_Controller.CharacterStatus.Pirate) 
		{

		}
	}
}
