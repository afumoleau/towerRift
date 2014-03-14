using UnityEngine;
using System.Collections;

public class myInterface : MonoBehaviour {

	public int maxHealth = 100;
	public float currentHealth;

	void OnGUI(){

		GUI.Button(new Rect(0,0,100,30), currentHealth + "/" + maxHealth);
	}

	void onMouseOver(){
		Debug.Log ("coucou");
	}
}
