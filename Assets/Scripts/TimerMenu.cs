using UnityEngine;
using System.Collections;

public class TimerMenu : MonoBehaviour {

	private int timeAccept;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.guiText.text = "ALLO ";
	}

	public void setTime (float second) {
		//this.guiText.text = ((int)second).ToString();
	}
}
