using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	void Start ()
	{
		Screen.SetResolution(640,480, false);
	}
	
	void Update ()
	{
	}

	void OnGUI ()
	{
		float buttonWidth = 300;
		if (GUI.Button (new Rect ((Screen.width - buttonWidth)/2, Screen.height/2 - 50, buttonWidth, 50), "Start - Standard Player Screen"))
		{
			PlayerPrefs.SetInt("gameMode", 0);
			Application.LoadLevel(1);
		}
		if (GUI.Button (new Rect ((Screen.width - buttonWidth)/2, Screen.height/2 + 10, buttonWidth, 50), "Start - Rift Player Screen"))
		{
			PlayerPrefs.SetInt("gameMode", 1);
			Application.LoadLevel(1);
		}
		if (GUI.Button (new Rect ((Screen.width - buttonWidth)/2, Screen.height/2 + 70, buttonWidth, 50), "Start - Both Screens"))
		{
			PlayerPrefs.SetInt("gameMode", 2);
			Application.LoadLevel(1);
		}
		if (GUI.Button (new Rect ((Screen.width - buttonWidth)/2, Screen.height/2 + 130, buttonWidth, 50), "Exit"))
		{
			Application.Quit();
		}
	}
}
