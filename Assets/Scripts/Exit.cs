using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	public AudioClip soundMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseUp () { 
		exitGame ();
	}

	public void exitGame () {
		audio.PlayOneShot(soundMenu);
		Application.Quit();
	}
}
