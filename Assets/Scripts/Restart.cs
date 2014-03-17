using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	
	public AudioClip soundMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() { 
		restartGame ();
	}

	public void restartGame () {
		audio.PlayOneShot(soundMenu);
		Application.LoadLevel("scene2");
		Time.timeScale = 1;
	}
}
