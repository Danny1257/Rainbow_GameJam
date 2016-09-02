using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour {

	private AudioSource explosionSound;
	private ParticleSystem explosionParticles;
	private float DestroyTimer;

	// Use this for initialization
	void Start () 
	{
		explosionSound = transform.GetComponentInChildren<AudioSource> ();
		explosionParticles = transform.GetComponentInChildren<ParticleSystem> ();
		DestroyTimer = 1.0f;

		explosionSound.Play ();
		explosionParticles.Play ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		DestroyTimer -= Time.deltaTime;

		if (DestroyTimer <= 0)
			Destroy (this.gameObject);
	}
}
