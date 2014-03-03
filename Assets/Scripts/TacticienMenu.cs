using UnityEngine;
using System.Collections;

public class TacticienMenu : MonoBehaviour {

	public bool stop;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		int menuSizeX = Screen.width;
		int menuSizeY = Screen.height;
		int menuPosX = 10;
		int menuPosY = 10;//Screen.height/2 - menuSizeY/2;
		int sizeButtonX = 250;
		int sizeButtonY = 30;
		Rect mainMenu = new Rect(menuPosX, menuPosY, menuSizeX, menuSizeY);
		
		GUI.BeginGroup (mainMenu, "");
		GUI.Box (new Rect(0, 0, menuSizeX/4 - 10, menuSizeY - 20), "Actions");
		
		if (GUI.Button (new Rect (10,30, sizeButtonX, sizeButtonY), "Action 1")) {

		}
		if (GUI.Button (new Rect (10, 70, sizeButtonX, sizeButtonY), "Action 2")) {

		}
		if (GUI.Button (new Rect (10, 110, sizeButtonX, sizeButtonY), "Action 3")) {
			
		}
		if (GUI.Button (new Rect (10, 150, sizeButtonX, sizeButtonY), "Action 4")) {
			
		}

		GUI.Box (new Rect ((menuSizeX / 4 * 3) - 10, 0, menuSizeX / 4 - 10, menuSizeY / 4 * 3 - 20), "Menu");

		if (GUI.Button (new Rect (menuSizeX / 4 * 3, 30, sizeButtonX, sizeButtonY), "Pause / Reprendre")) {
			/*if (stop == false)
				stop = true;
			else
				stop = false;*/
		}
		if (GUI.Button (new Rect (menuSizeX / 4 * 3, 70, sizeButtonX, sizeButtonY), "Quitter")) {
			
		}
		GUI.EndGroup ();
	}
}
