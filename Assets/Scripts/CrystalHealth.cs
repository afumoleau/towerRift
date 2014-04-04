using UnityEngine;
using System.Collections;

public class CrystalHealth : MonoBehaviour {

	public float crystalHealth;
	private const float maxHealth = 100.0f;
	private MenuPlayer menu;
	private bool stop = false;
	// Use this for initialization
	void Start () {
		crystalHealth = 100f;
		menu = GameObject.Find("Camera").GetComponent<MenuPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (crystalHealth <= 0.0f && !stop) {
			Debug.Log ("Game Over !");
			GameOver();
			stop = true;
		}
	}

	public void hit(float damage){
		crystalHealth -= damage;
	}

	private void GameOver(){
		menu.RestartGame ();
	}
}
