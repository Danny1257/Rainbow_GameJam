using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour 
{
	public Transform player;
    public float speed = 4f;

	private AudioSource backgroundMusic;
	private float offsetX, offsetY;
	// Use this for initialization
	void Start () 
	{
		offsetX = transform.position.x - player.position.x;
		offsetY = transform.position.y - player.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (player != null)
		{
            
            
			
            

            gameObject.transform.position = Vector3.Lerp((gameObject.transform.position), (player.position + new Vector3(0.3f, 2.53f, -8.59f)), Time.deltaTime * speed);
            Vector3 pos = transform.position;
            //pos.x = player.position.x + offsetX;
            //pos.y = player.position.y + offsetY;



            if (pos.y < -3.5f)
            {
                pos.y = -3.5f;
            }
            transform.position = pos;

        }
		else
			Debug.LogError("Player is null");
	}
}
