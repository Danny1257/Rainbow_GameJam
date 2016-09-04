using UnityEngine;
using System.Collections;

public class Bomb_Aim_Cursor : MonoBehaviour 
{
	public GameObject BombAim_Cursor;

	private Player_Controller playerController;
	private GameObject Aim_Cursor;


	// Use this for initialization
	void Start () 
	{
		playerController = transform.GetComponentInChildren<Player_Controller> ();
		Cursor.visible = false;
		//Aim_Cursor = Instantiate (BombAim_Cursor);
		//Aim_Cursor.transform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerController.GetCurrentStatus () == Player_Controller.CharacterStatus.Pirate) 
		{
			BombAim_Cursor.transform.position = Input.mousePosition;
			BombAim_Cursor.SetActive (true);
		} 
		else 
			BombAim_Cursor.SetActive (false);
	}
}
