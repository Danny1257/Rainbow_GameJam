using UnityEngine;
using System.Collections;

public class Character_Swap : MonoBehaviour {

	public RuntimeAnimatorController Wizard_animator, Pirate_Animator, Astronaut_Animator;
	public Avatar Wizard_Avatar, Pirate_Avatar, Astronaut_Avatar;
	public GameObject WizardMesh_Object, PirateMesh_Object, AstronautMesh_Object;
	public GameObject Jetpack;


	public GameObject currentMeshObject, EmptyPowerBar, PowerBar;
	private Animator currentPlayerAnimator;
	private Player_Controller playerController;
	private bool NeedChange;


	// Use this for initialization
	void Start () 
	{
		currentPlayerAnimator = transform.GetComponentInChildren<Animator> ();
		playerController = transform.GetComponentInChildren<Player_Controller> ();
		EmptyPowerBar = transform.FindChild ("BombUI").gameObject;
		PowerBar = transform.FindChild ("PowerBar").gameObject;
		NeedChange = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (NeedChange) 
		{
			if (playerController.GetCurrentStatus () == Player_Controller.CharacterStatus.Wizard) 
			{
				GameObject wizard_Object = Instantiate (WizardMesh_Object);
				wizard_Object.transform.parent = transform;
				wizard_Object.transform.localScale = currentMeshObject.transform.localScale;
				wizard_Object.transform.localPosition = new Vector3(0, 0, 0);
				Destroy (currentMeshObject);
				currentMeshObject = wizard_Object;

				currentPlayerAnimator.runtimeAnimatorController = Wizard_animator;
				currentPlayerAnimator.avatar = Wizard_Avatar;

				Jetpack.SetActive(false);

			} 
			else if (playerController.GetCurrentStatus () == Player_Controller.CharacterStatus.Pirate) 
			{
				GameObject pirate_Object = Instantiate(PirateMesh_Object);
				pirate_Object.transform.parent = transform;
				pirate_Object.transform.localScale = currentMeshObject.transform.localScale;
				pirate_Object.transform.localPosition = new Vector3(0, 0, 0);
				Destroy (currentMeshObject);
				currentMeshObject = pirate_Object;
			
				currentPlayerAnimator.runtimeAnimatorController = Pirate_Animator;
				currentPlayerAnimator.avatar = Pirate_Avatar;

				Jetpack.SetActive(false);
			} 
			else if (playerController.GetCurrentStatus () == Player_Controller.CharacterStatus.Astronaut) 
			{
				GameObject astronaut_Object = Instantiate(AstronautMesh_Object);
				astronaut_Object.transform.parent = transform;
				astronaut_Object.transform.localScale = currentMeshObject.transform.localScale;
				astronaut_Object.transform.localPosition = new Vector3(0, 0, 0);
				Destroy (currentMeshObject);
				currentMeshObject = astronaut_Object;

				currentPlayerAnimator.runtimeAnimatorController = Astronaut_Animator;
				currentPlayerAnimator.avatar = Astronaut_Avatar;
				Jetpack.SetActive(true);
			}

			// Disable power bar.
			if (EmptyPowerBar.activeSelf == true)
			{
				EmptyPowerBar.SetActive(false);
				PowerBar.SetActive(false);
			}


			NeedChange = false;
		}
	}

	public void PersonalityChange()
	{
		NeedChange = true;
	}
}
