using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Controller : MonoBehaviour 
{
	// Public members

	public Material Blue, Red, Yellow;

	// Private Members
	private enum CharacterStatus{ Default, Wizard, Astronaut, Pirate };
	private CharacterStatus CurrentStatus;
	private List<CharacterStatus> PersonalitiesList = new List<CharacterStatus>();

	private Wizard_Teleport wizard_teleport;
	private TrailRenderer wizard_trail;

	private Astronaut_Fly astronaut_Fly;
	private Pirate_Bomb pirate_bomb;

	private int PersonalitiesCollected;

	// Use this for initialization
	void Start ()
	{
		wizard_teleport = transform.GetComponentInChildren<Wizard_Teleport>();
		astronaut_Fly = transform.GetComponentInChildren<Astronaut_Fly>();
		pirate_bomb = transform.GetComponentInChildren<Pirate_Bomb>();
		wizard_trail = transform.GetComponentInChildren<TrailRenderer>();
		CurrentStatus = CharacterStatus.Default;
		PersonalitiesCollected = 0;
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
		}

		if (Input.GetMouseButtonDown(0))
		{
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
			pirate_bomb.StartThrow();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			pirate_bomb.EndThrow();
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

		// Change the player model/material
		if (CurrentStatus == CharacterStatus.Wizard)
		{
			transform.GetComponent<Renderer>().material = Blue;
		}
		else if (CurrentStatus == CharacterStatus.Astronaut)
		{
			transform.GetComponent<Renderer>().material = Red;
		}
		else if (CurrentStatus == CharacterStatus.Pirate)
		{
			transform.GetComponent<Renderer>().material = Yellow;
		}
	}

	public void Pickup(int pickUpNumber)
	{
		if (pickUpNumber == 1)
		{
			PersonalitiesList.Add(CharacterStatus.Wizard);
			CurrentStatus = CharacterStatus.Wizard;

			// Change the players colour / model to wizard character
			transform.GetComponent<Renderer>().material = Blue;
		}
		else if (pickUpNumber == 2)
		{
			PersonalitiesList.Add(CharacterStatus.Astronaut);
			CurrentStatus = CharacterStatus.Astronaut;
			
			// Change the players colour / model to wizard character
			transform.GetComponent<Renderer>().material = Red;
		}

		else if (pickUpNumber == 3)
		{
			PersonalitiesList.Add(CharacterStatus.Pirate);
			CurrentStatus = CharacterStatus.Pirate;
			
			// Change the players colour / model to wizard character
			transform.GetComponent<Renderer>().material = Yellow;
		}
	}
}
