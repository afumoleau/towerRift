using UnityEngine;
using System.Collections;

/// <summary>
///  Class "MenuPlayer" displays the main menu of the player with the Oculus Rift & Kinect.
/// </summary>
public class OculusPlayerMenu : MonoBehaviour {

	public GameObject head;
	public GameObject left_hand;
	public GameObject right_hand;
	public GameObject menuInGame;
	public int heightMenu;
	public int distanceButtons;
	public int time;
	public Material on;
	public Material off;

	private GameObject myResume;
	private GameObject myRestart;
	private GameObject myExit;
	private GameObject myTimer;
	private float localHeightMenu;
	private float timeOk;
	private bool stop = false;
	private int select = 0;
	private float timer = 0;

	/// <summary>
	/// Main loop that handles the events to launch the menu with the kinect.
	/// </summary>
	void Update () {
		timer += Time.fixedDeltaTime;
		ActionDisplayMenu ();
		if (stop)
			ActionInMenu ();
	}

	/// <summary>
	/// Function that displays the next menu or not if the left hand is in the air or not.
	/// </summary>
	void ActionDisplayMenu () {
		if (left_hand.transform.position.y > head.transform.position.y && stop == false) {
			Debug.Log ("Main gauche au dessus !");
			Time.timeScale = 0;
			menuInGame.SetActive(true);
			stop = true;
		}
		else if (left_hand.transform.position.y < head.transform.position.y && stop == true ) {
			Debug.Log ("Main gauche en dessous !");
			Time.timeScale = 1;
			menuInGame.SetActive(false);
			stop = false;
			select = 0;
		}
	}

	/// <summary>
	/// Function that prints and applies a selection of menu buttons with the right hand.
	/// </summary>
	void ActionInMenu () { 
		myResume = GameObject.Find("Resume");
		myRestart = GameObject.Find("Restart");
		myExit = GameObject.Find("Exit");

		localHeightMenu = head.transform.position.y + heightMenu;

		if ((right_hand.transform.position.y > localHeightMenu - distanceButtons) && select != 1) {
			myResume.renderer.material = on;
			myRestart.renderer.material = off;
			myExit.renderer.material = off;
			select = 1;
			timeOk = timer;
		} else if ((right_hand.transform.position.y > localHeightMenu - 2 * distanceButtons) && (right_hand.transform.position.y < (localHeightMenu - distanceButtons)) && select != 2) {
			myResume.renderer.material = off;
			myRestart.renderer.material = on;
			myExit.renderer.material = off;
			select = 2;
			timeOk = timer;
		} else if ((right_hand.transform.position.y > localHeightMenu - 3 * distanceButtons) && (right_hand.transform.position.y < localHeightMenu - 2 * distanceButtons) && select != 3) {
			myResume.renderer.material = off;
			myRestart.renderer.material = off;
			myExit.renderer.material = on;
			select = 3;
			timeOk = timer;
		} else if ((right_hand.transform.position.y < localHeightMenu - 3 * distanceButtons) && select != 0) {
			myResume.renderer.material = off;
			myRestart.renderer.material = off;
			myExit.renderer.material = off;
			select = 0;
		}

		if ((timer > timeOk + time) && select != 0) {
			float timeDisplay = timeOk + time;
			switch (select) {
				case 1:
					menuInGame.SetActive(false);
					Time.timeScale = 1;
					break;
				case 2:
					menuInGame.SetActive(false);
					Application.LoadLevel("scene2");
					Time.timeScale = 1;
					break;
				case 3:
					Application.Quit();
					break;
			}
		}
	}
}

