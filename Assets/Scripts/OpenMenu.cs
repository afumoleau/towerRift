using UnityEngine;
using System.Collections;

public class OpenMenu : MonoBehaviour {

	public GameObject head;
	public GameObject left_hand;
	public GameObject right_hand;
	public GameObject menuInGame;
	public int distanceMenu;
	public int distanceArm;
	public Material on;
	public Material off;

	private GameObject myResume;
	private GameObject myRestart;
	private GameObject myExit;	
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
			//animation.Play();
		}
	}

	void ActionInMenu () { 
		myResume = GameObject.Find("Resume");
		myRestart = GameObject.Find("Restart");
		myExit = GameObject.Find("Exit");

		myResume.renderer.material = off;
		myRestart.renderer.material = off;
		myExit.renderer.material = off;

		if (right_hand.transform.position.y > head.transform.position.y + distanceMenu) {
			myResume.renderer.material = on;
			select = 1;
		} else if (right_hand.transform.position.y > head.transform.position.y - (distanceMenu / 2)) {
			myRestart.renderer.material = on;
			select = 2;
		} else {
			myExit.renderer.material = on;
			select = 3;
		}

		if (right_hand.transform.position.z > head.transform.position.z + distanceArm) {

			switch (select) {
			case 1:
				/*Resume myResume2 = ((Resume)myResume.gameObject.GetComponent("Resume"));
				myResume2.resumeGame ();*/
				((Resume)myResume.gameObject.GetComponent("Resume")).resumeGame ();

				break;
			case 2:
				/*Restart myRestart2 = ((Restart)myRestart.gameObject.GetComponent("Restart"));
				myRestart2.restartGame ();*/
				((Restart)myResume.gameObject.GetComponent("Restart")).restartGame ();
				break;
			case 3:
				/*Exit myExit2 = ((Exit)myExit.gameObject.GetComponent("Exit"));
				myExit2.quitGame ();*/
				((Exit)myExit.gameObject.GetComponent("Exit")).exitGame ();
				break;
			}
		}
	}
}

