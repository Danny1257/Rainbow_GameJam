using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour 
{
	// Public members
	public Material Blue, Red;

	// Private Members
	private enum CharacterStatus{ Default, Wizard, Astronaut, Pirate };
	CharacterStatus CurrentStatus;
	// Use this for initialization
	void Start ()
	{
		CurrentStatus = CharacterStatus.Default;
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

	}

	private void AstronautUpdate()
	{

	}

	private void PirateUpdate()
	{

	}

	public void Pickup()
	{
		CurrentStatus = CharacterStatus.Wizard;

		//
	}
}
