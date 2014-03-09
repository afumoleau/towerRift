using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour {
	
	public AudioClip soundMenu;
	public GameObject menuInGame;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() { 
		Debug.Log ("Resume !");
		audio.PlayOneShot(soundMenu);
		menuInGame.SetActive(false);
		Time.timeScale = 1;
	}
}
