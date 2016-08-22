using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {

	public float speed = 1;

	private int direction = 1;


	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.left * speed * direction * Time.deltaTime);

	
	}

	void OnTriggerEnter(Collider other){



		if (other.tag == "Target") {
			if (direction == 1) {
				Debug.Log ("Direction = " + direction);
				direction = -1;
			} else {
				direction = 1;
			}
		}

	}
}
