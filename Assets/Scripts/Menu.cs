using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public Material on;
	public Material off;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter () {
		renderer.material = on;
	}

	void OnMouseExit () {
		renderer.material = off;
	}
}