using UnityEngine;
using System.Collections;

public class Pirate_Bomb : MonoBehaviour 
{
	public GameObject AimObject, Bomb;
	public float MaxForce;
	private Rigidbody body;
	private Vector3 mousePos, screenPos;
	private GameObject currentBomb;
	private bool BombSpawned, BombReleased;
	private float force;
	
	// Use this for initialization
	void Start () 
	{
		//body = AimObject.transform.GetComponentInChildren<Rigidbody>();
		BombSpawned = false;
		BombReleased = false;
		force = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (BombSpawned)
		{
			if (force < MaxForce)
			{
				force += Time.deltaTime * 100;
			}
			
			
			if (BombReleased)
			{
				RaycastHit hit;
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
			}
		}
		
		
		
		
		
		/* HOW TO FIX
		 * Find both vectors for current rotation of aim object, and rotation vector of center of player to mouse position.
		 * Find angle between both vectors, then rotate around the player position by that angle.
		 */
		/*mousePos = Input.mousePosition;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Plane zPlane = new Plane(Vector3.forward, Vector3.zero);

		float distance = 0;

		if (zPlane.Raycast(ray, out distance))
		{
			screenPos = ray.GetPoint(distance);
		}

		Vector3 Vect1, Vect2;
		
		Vect1 = screenPos - transform.position;
		Vect2 = AimObject.transform.position - transform.position;
		
		float angle = Vector3.Angle(Vect1, Vect2);

		//AimObject.transform.LookAt(screenPos);


		//AimObject.transform.rotation.eulerAngles = new Vector3(0, 0, angle);*/
		
		
		
		
		/*Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

		float distance = Camera.main.transform.position.z * -2;

		Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(cameraRay.GetPoint(distance));

		MouseWorldPos = new Vector3(MouseWorldPos.x, MouseWorldPos.y, 0);

		Debug.Log("MouseWorldPos = " + MouseWorldPos);
		*/
		
		
		//AimObject.transform.RotateAround(transform.position, new Vector3(0, 0, 1), angle);
		
		
		//mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
		//screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));
		
		//float rotion_amount = Quaternion.Euler(0, 0, Mathf.Atan2((mousePos.y - AimObject.transform.position.y), (mousePos.x - AimObject.transform.position.x))*Mathf.Rad2Deg - 90).z;
		//AimObject.transform.RotateAround(transform.position, new Vector3(0, 0, 1), rotion_amount);
		
		//body.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x))*Mathf.Rad2Deg - 90);
		
		//AimObject.transform.rotation.eulerAngles = new Vector3(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);
	}
	
	public void StartThrow()
	{
		force = 0;
		currentBomb = Instantiate(Bomb);
		BombSpawned = true;
		BoxCollider playerBox = transform.GetComponentInChildren<BoxCollider>();
		
		currentBomb.transform.position = new Vector3(transform.position.x + (playerBox.size.x/2) + 0.1f, transform.position.y, transform.position.z);
		currentBomb.transform.GetComponentInChildren<Rigidbody>().useGravity = false;
	}
	
	public void EndThrow()
	{
		BombReleased = true;
		
	}
}
