using UnityEngine;
using System.Collections;

public class TransitionScene : MonoBehaviour {

	private float transitionTimer;
	// Use this for initialization
	void Start () 
	{
		transitionTimer = 8.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transitionTimer -= Time.deltaTime;

		if (transitionTimer <= 0) {
			Application.LoadLevel(6);
		}
	}
}
