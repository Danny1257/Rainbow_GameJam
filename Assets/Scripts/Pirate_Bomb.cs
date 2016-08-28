using UnityEngine;
using System.Collections;

public class Pirate_Bomb : MonoBehaviour 
{
	public GameObject AimObject, Bomb, EmptyBar, PowerBar;
	public float MaxForce;


	private Rigidbody body;
	private Vector3 mousePos, screenPos;
	private GameObject currentBomb;
	private bool BombSpawned, BombReleased, BombReady, StartTimer;
	private float force, DestroyTimer;
	private GameObject Current_ExplosionZone_Object;
	private Explosion_Zone explosion_zone;
	private float MaxBarScale;
	
	// Use this for initialization
	void Start () 
	{
		//body = AimObject.transform.GetComponentInChildren<Rigidbody>();
		BombSpawned = false;
		BombReleased = false;
		BombReady = true;
		StartTimer = false;
		force = 0.0f;
		DestroyTimer = 2.0f;
		MaxBarScale = PowerBar.transform.localScale.x;
		PowerBar.transform.localScale = new Vector3(0, PowerBar.transform.localScale.y, PowerBar.transform.localScale.z);
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
				force += Time.deltaTime * 100;

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
				
				Vector3 force_Vec = difference * force;
				
				float radius = currentBomb.GetComponentInChildren<SphereCollider>().radius;
				Vector3 ForcePos = new Vector3(currentBomb.transform.position.x - radius, currentBomb.transform.position.y, currentBomb.transform.position.z);
				
				bomb_body.AddForceAtPosition(force_Vec, ForcePos);
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
					Debug.Log ("The explosion hit! Target Aactive!");
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

		force = 0;
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
