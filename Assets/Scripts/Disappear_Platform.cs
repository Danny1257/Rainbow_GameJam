using UnityEngine;
using System.Collections;

public class Disappear_Platform : MonoBehaviour {

	public GameObject platform1;
	public GameObject platform2;

	private float timer = 5;
	

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		if (timer <= 2.5) {

			platform1.SetActive (false);
		}

		if (timer <= 0) {

			platform1.SetActive (true);

			timer = 5;
		}

		if (timer <= 3) {
			
			platform2.SetActive (true);
			
		}

		if (timer <= 0.5) {
			
			platform2.SetActive (false);
		}
		
	

	
	}
}
