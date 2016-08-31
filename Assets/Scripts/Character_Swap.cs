using UnityEngine;
using System.Collections;

public class Character_Swap : MonoBehaviour {

	public RuntimeAnimatorController Wizard_animator, Pirate_Animator, Astronaut_Animator;
	public GameObject WizardMesh_Object, PirateMesh_Object, AstronautMesh_Object;


	public GameObject currentMeshObject;
	private RuntimeAnimatorController currentPlayerAnimator;
	private Player_Controller playerController;
	private bool NeedChange;


	// Use this for initialization
	void Start () 
	{
		currentPlayerAnimator = transform.GetComponentInChildren<Animator> ().runtimeAnimatorController;
		playerController = transform.GetComponentInChildren<Player_Controller> ();
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

				currentPlayerAnimator = Wizard_animator;
			} 
			else if (playerController.GetCurrentStatus () == Player_Controller.CharacterStatus.Pirate) 
			{
				GameObject pirate_Object = Instantiate(PirateMesh_Object);
				pirate_Object.transform.parent = transform;
				pirate_Object.transform.localScale = currentMeshObject.transform.localScale;
				pirate_Object.transform.localPosition = new Vector3(0, 0, 0);
				Destroy (currentMeshObject);
				currentMeshObject = pirate_Object;
			
				currentPlayerAnimator = Pirate_Animator;
			} 
			else if (playerController.GetCurrentStatus () == Player_Controller.CharacterStatus.Astronaut) 
			{
				GameObject astronaut_Object = Instantiate(AstronautMesh_Object);
				astronaut_Object.transform.parent = transform;
				astronaut_Object.transform.localScale = currentMeshObject.transform.localScale;
				astronaut_Object.transform.localPosition = new Vector3(0, 0, 0);
				Destroy (currentMeshObject);
				currentMeshObject = astronaut_Object;

				currentPlayerAnimator = Astronaut_Animator;
			}

			NeedChange = false;
		}
	}

	public void PersonalityChange()
	{
		NeedChange = true;
	}
}
