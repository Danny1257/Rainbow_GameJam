using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Controller : MonoBehaviour 
{
	// Public members

	public Material Blue, Red, Yellow;
	public List<GameObject> CheckPoints = new List<GameObject>();
	public List<GameObject> PickUps_List = new List<GameObject>();
	public GameObject PickUpObject;
	public ParticleSystem CharacterTransformSystem;

	// Private Members
	private enum CharacterStatus{ Default, Wizard, Astronaut, Pirate };
	private CharacterStatus CurrentStatus;
	private List<CharacterStatus> PersonalitiesList = new List<CharacterStatus>();

	private Wizard_Teleport wizard_teleport;
	private Astronaut_Fly astronaut_Fly;
	private Pirate_Bomb pirate_bomb;
	private int ActiveCheckpoint;
	private int NumOfPickups;
	private Vector3 LastPickUpPos;
	
	// Use this for initialization
	void Start ()
	{
		wizard_teleport = transform.GetComponentInChildren<Wizard_Teleport>();
		astronaut_Fly = transform.GetComponentInChildren<Astronaut_Fly>();
		pirate_bomb = transform.GetComponentInChildren<Pirate_Bomb>();
		CurrentStatus = CharacterStatus.Default;
		NumOfPickups = 0;
		ActiveCheckpoint = 0;
		LastPickUpPos = new Vector3(0, 0, 0);

		if (Application.loadedLevelName == "Level2")
		{
			if (!PersonalitiesList.Contains(CharacterStatus.Wizard))
				PersonalitiesList.Add(CharacterStatus.Wizard);
			CurrentStatus = CharacterStatus.Wizard;
		}
		else if (Application.loadedLevelName == "Level3")
		{
			if (!PersonalitiesList.Contains(CharacterStatus.Wizard))
				PersonalitiesList.Add(CharacterStatus.Wizard);

			if (!PersonalitiesList.Contains(CharacterStatus.Pirate))
				PersonalitiesList.Add (CharacterStatus.Pirate);
			CurrentStatus = CharacterStatus.Pirate;
		}
		else if (Application.loadedLevelName == "Level4")
		{
			if (!PersonalitiesList.Contains(CharacterStatus.Wizard))
				PersonalitiesList.Add(CharacterStatus.Wizard);
			
			if (!PersonalitiesList.Contains(CharacterStatus.Pirate))
				PersonalitiesList.Add (CharacterStatus.Pirate);

			if (!PersonalitiesList.Contains(CharacterStatus.Astronaut))
				PersonalitiesList.Add (CharacterStatus.Astronaut);
			CurrentStatus = CharacterStatus.Astronaut;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(CurrentStatus)
		{
		case CharacterStatus.Default:
			DefaultUpdate();
			break;
		case CharacterStatus.Wizard:
			WizardUpdate();
			break;
		case CharacterStatus.Astronaut:
			AstronautUpdate();
			break;
		case CharacterStatus.Pirate:
			PirateUpdate();
			break;
		}
	}

	private void DefaultUpdate()
	{
			
	}

	private void WizardUpdate()
	{
		if (PersonalitiesList.Count > 1)
		{
			CheckForPersonalityChange();
			if (CurrentStatus != CharacterStatus.Wizard)
				wizard_teleport.SetPointLight(false);
			else
				wizard_teleport.SetPointLight(true);
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (wizard_teleport.GetTeleportReady())
				wizard_teleport.Teleport();
		}


	}

	private void AstronautUpdate()
	{
		if (PersonalitiesList.Count > 1)
		{
			CheckForPersonalityChange();
		}

		if (Input.GetMouseButton(0))
		{
			astronaut_Fly.Fly();
		}

		if (Input.GetMouseButtonUp(0))
		{
			astronaut_Fly.StartTimer();
			astronaut_Fly.StopParticles();
		}
	}

	private void PirateUpdate()
	{
		if (PersonalitiesList.Count > 1)
		{
			CheckForPersonalityChange();
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (pirate_bomb.GetBombReady())
				pirate_bomb.StartThrow();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (pirate_bomb.GetBombReady())
			{
				if (pirate_bomb.GetBombSpawned())
					pirate_bomb.EndThrow();
			}
		}
	}

	private void CheckForPersonalityChange()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (CurrentStatus == CharacterStatus.Wizard)
			{
				if (PersonalitiesList.Contains(CharacterStatus.Astronaut))
				    CurrentStatus = CharacterStatus.Astronaut;
				else
					CurrentStatus = CharacterStatus.Pirate;
			}				
			else if (CurrentStatus == CharacterStatus.Astronaut)
			{
				if (PersonalitiesList.Contains(CharacterStatus.Pirate))
					CurrentStatus = CharacterStatus.Pirate;
				else 
					CurrentStatus = CharacterStatus.Wizard;
			}				
			else if (CurrentStatus == CharacterStatus.Pirate)
			{
				if (PersonalitiesList.Contains(CharacterStatus.Wizard))
					CurrentStatus = CharacterStatus.Wizard;
				else
					CurrentStatus = CharacterStatus.Astronaut;
			}

			CharacterTransformSystem.Play();
				
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (CurrentStatus == CharacterStatus.Wizard)
			{
				if (PersonalitiesList.Contains(CharacterStatus.Pirate))
					CurrentStatus = CharacterStatus.Pirate;
				else
					CurrentStatus = CharacterStatus.Astronaut;
			}				
			else if (CurrentStatus == CharacterStatus.Astronaut)
			{
				if (PersonalitiesList.Contains(CharacterStatus.Wizard))
					CurrentStatus = CharacterStatus.Wizard;
				else
					CurrentStatus = CharacterStatus.Pirate;
			}				
			else if (CurrentStatus == CharacterStatus.Pirate)
			{
				if (PersonalitiesList.Contains(CharacterStatus.Astronaut))
					CurrentStatus = CharacterStatus.Astronaut;
				else 
					CurrentStatus = CharacterStatus.Wizard;
			}				
		}
	}

	public void Pickup(int pickUpNumber, Vector3 pickupPos)
	{
		NumOfPickups++;

		LastPickUpPos = pickupPos;

		if (NumOfPickups == 3)
		{
			if (Application.loadedLevelName == "Scene")
			{
				if (!PersonalitiesList.Contains(CharacterStatus.Pirate))
					PersonalitiesList.Add(CharacterStatus.Pirate);
				CurrentStatus = CharacterStatus.Pirate;
			}
			else if (Application.loadedLevelName == "Level2")
			{
				if (!PersonalitiesList.Contains(CharacterStatus.Pirate))
					PersonalitiesList.Add(CharacterStatus.Pirate);
				CurrentStatus = CharacterStatus.Pirate;
			}
			else if (Application.loadedLevelName == "Level3")
			{
				if (!PersonalitiesList.Contains(CharacterStatus.Astronaut))
					PersonalitiesList.Add(CharacterStatus.Astronaut);
				CurrentStatus = CharacterStatus.Astronaut;
			}
		}

	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.name == "CheckPoint_2")
		{
			if (NumOfPickups == 1)
				ActiveCheckpoint = 1;

		}
		else if (collider.name == "CheckPoint_3")
		{
			if (NumOfPickups == 2)
				ActiveCheckpoint = 2;
		}
		else if (collider.name == "CheckPoint_4")
		{
			if (NumOfPickups == 3)
				ActiveCheckpoint = 3;
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.name == "CheckPoint_2")
		{
			if (NumOfPickups == 1)
				ActiveCheckpoint = 1;			
		}
		else if (collider.name == "CheckPoint_3")
		{
			if (NumOfPickups == 2)
				ActiveCheckpoint = 2;
		}
		else if (collider.name == "CheckPoint_4")
		{
			if (NumOfPickups == 3)
				ActiveCheckpoint = 3;
		}
	}

	public int GetNumOfPickups()
	{
		return NumOfPickups;
	}


	public void Death()
	{
		Debug.Log("Death");
		// Spawn player at the active checkPoint
		Transform checkpointTransform = CheckPoints[ActiveCheckpoint].transform;
		Vector3 newPos = new Vector3(checkpointTransform.position.x, checkpointTransform.position.y, 0);
		transform.position = newPos;

		// Check if a pickup needs to be respawned.	

		if (NumOfPickups > ActiveCheckpoint)
		{
			Debug.Log("Respawn Pickup");
			NumOfPickups--;
			GameObject newPickup = Instantiate(PickUpObject);
			newPickup.transform.position = LastPickUpPos;
		}

		if (PersonalitiesList.Contains(CharacterStatus.Pirate))
		{
			pirate_bomb.Death(ActiveCheckpoint);
		}
	}
}
