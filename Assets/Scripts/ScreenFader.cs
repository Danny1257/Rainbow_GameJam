using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	public Texture2D fadeOutTexture; 
	public float fadeSpeed = 0.8f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1; 

	public void OnGUI () {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r,GUI.color.g, GUI.color.g, alpha );
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);

	}

	public float BeginFade(int direction) {
		fadeDir = direction;
		return (fadeSpeed);
	
	}

	public void OnLevelWasLoaded() {
		BeginFade(-1);
	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.Z)){
			BeginFade (1);
		}
		if(Input.GetKeyDown(KeyCode.X)){
			BeginFade (-1);
		}
	}


}
