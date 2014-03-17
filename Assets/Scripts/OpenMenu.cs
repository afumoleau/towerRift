using UnityEngine;
using System.Collections;

public class OpenMenu : MonoBehaviour {

	public GameObject head;
	public GameObject left_hand;
	public GameObject right_hand;
	public GameObject menuInGame;
	public int distanceMenu;
	public int time;
	public Material on;
	public Material off;

	private GameObject myResume;
	private GameObject myRestart;
	private GameObject myExit;
	private GameObject myTimer;
	private float timeOk;
	private bool stop = false;
	private int select = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		ActionDisplayMenu ();
		if (stop)
			ActionInMenu ();
	}

	void ActionDisplayMenu () {
		if (left_hand.transform.position.y > head.transform.position.y && stop == false) {
			Time.timeScale = 0;
			menuInGame.SetActive(true);
			stop = true;
		}
		else if (left_hand.transform.position.y < head.transform.position.y && stop == true ) {
			Time.timeScale = 1;
			menuInGame.SetActive(false);
			stop = false;
			select = 0;
			//animation.Play();
		}
	}

	void ActionInMenu () { 
		myResume = GameObject.Find("Resume");
		myRestart = GameObject.Find("Restart");
		myExit = GameObject.Find("Exit");

		if ((right_hand.transform.position.y > head.transform.position.y + distanceMenu) && select != 1) {
			myResume.renderer.material = on;
			myRestart.renderer.material = off;
			myExit.renderer.material = off;
			select = 1;
			timeOk = Time.time;
		} else if ((right_hand.transform.position.y > head.transform.position.y - (distanceMenu / 2)) && select != 2) {
			myResume.renderer.material = off;
			myRestart.renderer.material = on;
			myExit.renderer.material = off;
			select = 2;
			timeOk = Time.time;
		} else if (select != 3){
			myResume.renderer.material = off;
			myRestart.renderer.material = off;
			myExit.renderer.material = on;
			select = 3;
			timeOk = Time.time;
		}

		if (Time.time > timeOk + time) {
			switch (select) {
				case 1:
					menuInGame.SetActive(false);
					Time.timeScale = 1;
					break;
				case 2:
					Application.LoadLevel("scene2");
					Time.timeScale = 1;
					break;
				case 3:
					Application.Quit();
					break;
			}
		}
		else {

		}
	}
}

