using UnityEngine;
using System.Collections;

public class Pirate_Bomb : MonoBehaviour 
{
	public GameObject AimObject;
	private Rigidbody body;
	private Vector3 mousePos, screenPos;

	// Use this for initialization
	void Start () 
	{
		body = AimObject.transform.GetComponentInChildren<Rigidbody>();
		mousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update ()
	{

		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
		//screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));

		float rotion_amount = Quaternion.Euler(0, 0, Mathf.Atan2((mousePos.y - AimObject.transform.position.y), (mousePos.x - AimObject.transform.position.x))*Mathf.Rad2Deg - 90).z;
		AimObject.transform.RotateAround(transform.position, new Vector3(0, 0, 1), rotion_amount);

		//body.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x))*Mathf.Rad2Deg - 90);

		//AimObject.transform.rotation.eulerAngles = new Vector3(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);
	}

	public void StartThrow()
	{

	}

	public void EndThrow()
	{

	}
}
