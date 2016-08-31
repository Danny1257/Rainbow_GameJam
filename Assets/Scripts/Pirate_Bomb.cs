using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pirate_Bomb : MonoBehaviour 
{
	public GameObject AimObject, Bomb, EmptyBar, PowerBar;
	public float MaxForce;
	public List<GameObject> BombTarget_List = new List<GameObject>();


	private Rigidbody body;
	private Vector3 mousePos, screenPos;
	private GameObject currentBomb;
	private bool BombSpawned, BombReleased, BombReady, StartTimer;
	private float force, DestroyTimer;
	private GameObject Current_ExplosionZone_Object;
	private Explosion_Zone explosion_zone;
	private float MaxBarScale, rate;
	
	// Use this for initialization
	void Start () 
	{
		//body = AimObject.transform.GetComponentInChildren<Rigidbody>();
		BombSpawned = false;
		BombReleased = false;
		BombReady = true;
		StartTimer = false;
		force = 200.0f;
		DestroyTimer = 2.0f;
		MaxBarScale = PowerBar.transform.localScale.x;
		//PowerBar.transform.localScale = new Vector3(0, PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);
		rate = MaxBarScale / 1.2f;
		Debug.Log("Max bar scale " + MaxBarScale);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (BombSpawned)
		{
			CapsuleCollider playerBox = transform.GetComponentInChildren<CapsuleCollider>();
			currentBomb.transform.position = new Vector3(transform.position.x + (playerBox.radius) + 0.1f, transform.position.y + + (playerBox.radius), transform.position.z);
			//currentBomb.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			if (force < MaxForce)
			{
				force += 250 * Time.deltaTime;
				Debug.Log("Force = " + force);

				// Alter power bar size
				PowerBar.transform.localScale = new Vector3(PowerBar.transform.localScale.x + (rate * Time.deltaTime), PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);
			}
			
			
			if (BombReleased)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				Plane zPlane = new Plane(Vector3.forward, Vector3.zero);
				
				float distance = 0;
				
				if (zPlane.Raycast(ray, out distance))
				{
					screenPos = ray.GetPoint(distance);
				}
				
				Rigidbody bomb_body = currentBomb.transform.GetComponentInChildren<Rigidbody>();
				
				
				/// Calculate the force vector
				Vector3 difference = screenPos - transform.position;
				
				Vector3 force_Vec = new Vector3(force, difference.y * force, 0);
				
				float radius = currentBomb.GetComponentInChildren<SphereCollider>().radius;
				Vector3 ForcePos = new Vector3(currentBomb.transform.position.x - radius, currentBomb.transform.position.y, currentBomb.transform.position.z);
				
				//bomb_body.AddForceAtPosition(force_Vec, ForcePos);

				Vector3 direction = (screenPos - transform.position).normalized;

				bomb_body.AddForce(direction * force);
				bomb_body.useGravity = true;
				
				BombSpawned = false;
				BombReleased = false;

				// Disbale bomb UI
				EmptyBar.SetActive(false);
				PowerBar.SetActive(false);

				StartTimer = true;
			}
		}

		if (StartTimer)
		{
			DestroyTimer -= Time.deltaTime;
			if (DestroyTimer <= 0)					////////// EXPLOSION POINT HERE!!!
			{
				explosion_zone = currentBomb.transform.GetComponentInChildren<Explosion_Zone>();

				if (explosion_zone.GetBombInTargetZone())
				{
					Debug.Log ("The explosion hit! Target Active!");
					SuccesfulExplosion();
				}
				else 
				{
					Debug.Log("The explosion missed the targets!");
				}

				Destroy(currentBomb);
				BombReady = true;
				StartTimer = false;
				DestroyTimer = 2.0f;
			}
		}
	}


	private void SuccesfulExplosion()
	{
		GameObject bombTarget = explosion_zone.GetLastHitTarget();
		bombTarget.GetComponentInChildren<Renderer>().enabled = false;
		bombTarget.GetComponentInChildren<SphereCollider>().enabled = false;

		if (bombTarget.GetComponentInChildren<BombTargetController> ().Platform.GetComponentInChildren<PlatformMovement> () != null) {

			PlatformMovement TriggerPlatform = bombTarget.GetComponentInChildren<BombTargetController> ().Platform.GetComponentInChildren<PlatformMovement> ();
			TriggerPlatform.SetBombTriggered(true);

		}

		if (bombTarget.GetComponentInChildren<BombTargetController> ().Platform.GetComponentInChildren<Disappear_TarPlatform> () != null) {
			Disappear_TarPlatform TriggerDisPlatform = bombTarget.GetComponentInChildren<BombTargetController> ().Platform.GetComponentInChildren<Disappear_TarPlatform> ();
			TriggerDisPlatform.SetBombTriggered (true);

		}

		if (bombTarget.GetComponentInChildren<BombTargetController> ().Platform.GetComponentInChildren<MoveablePlatform> () != null) {
			MoveablePlatform moveablePlatform = bombTarget.GetComponentInChildren<BombTargetController>().Platform.GetComponentInChildren<MoveablePlatform>();
			moveablePlatform.SetBombTrigger(true);
		}


	}

	public void Death(int activeCheckpoint)
	{
		for (int n = 0; n < BombTarget_List.Count; n++)
		{
			BombTargetController target_Controller = BombTarget_List[n].transform.GetComponentInChildren<BombTargetController>();
			if (target_Controller.StageOfLevel > activeCheckpoint)
			{
				Debug.Log("Reset the bomb target");
				BombTarget_List[n].transform.GetComponentInChildren<Renderer>().enabled = true;
				BombTarget_List[n].transform.GetComponentInChildren<SphereCollider>().enabled = true;


				if (BombTarget_List[n].transform.GetComponentInChildren<BombTargetController>().Platform.GetComponentInChildren<PlatformMovement>() != null){
				// Also reset the result of the target i.e moving back a platform etc
				PlatformMovement TriggerPlatform = BombTarget_List[n].transform.GetComponentInChildren<BombTargetController>().Platform.GetComponentInChildren<PlatformMovement>();
				TriggerPlatform.SetBombTriggered(false);
				TriggerPlatform.ResetPlatform();
				}

				if (BombTarget_List[n].transform.GetComponentInChildren<BombTargetController>().Platform.GetComponentInChildren<Disappear_TarPlatform>() != null){
				Disappear_TarPlatform TriggerDisPlatform = BombTarget_List[n].transform.GetComponentInChildren<BombTargetController>().Platform.GetComponentInChildren<Disappear_TarPlatform>();
				TriggerDisPlatform.SetBombTriggered(false);
				TriggerDisPlatform.ResetPlatform();
				}

			}
		}
	}

	public bool GetBombReady()
	{
		return BombReady;
	}

	public bool GetBombSpawned()
	{
		return BombSpawned;
	}

	public void SetBombReady(bool state)
	{
		BombReady = state;
	}

	public void StartThrow()
	{
		// Enable bomb throw UI
		EmptyBar.gameObject.SetActive(true);
		PowerBar.gameObject.SetActive(true);

		// Initialise Bomb UI settings i.e scale

		force = 200;
		PowerBar.transform.localScale = new Vector3(0, PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);
		currentBomb = Instantiate(Bomb);
		Current_ExplosionZone_Object = currentBomb.transform.GetChild(0).gameObject;
		BombSpawned = true;

		CapsuleCollider playerBox = transform.GetComponentInChildren<CapsuleCollider>();
		
		currentBomb.transform.position = new Vector3(transform.position.x + (playerBox.radius) + 0.1f, transform.position.y + (playerBox.radius), transform.position.z);
		currentBomb.transform.GetComponentInChildren<Rigidbody>().useGravity = false;
	}
	
	public void EndThrow()
	{
		BombReleased = true;
		BombReady = false;		
	}
}
