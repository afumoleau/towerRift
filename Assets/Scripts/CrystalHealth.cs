using UnityEngine;
using System.Collections;

public class CrystalHealth : MonoBehaviour {

	public float crystalHealth;
	private const float maxHealth = 100.0f;
	private bool showMenu;
	// Use this for initialization
	void Start () {
		crystalHealth = 100f;
		showMenu = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ( crystalHealth <= 0.0f) {
			GameOver();
		}
	}

	public void hit(float damage){
		crystalHealth -= damage;
	}

	private void GameOver(){
		showMenu = true;
	}

	public void OnGUI(){
		if( showMenu ){
			Time.timeScale = 0f;

			Component[] filters  = GetComponentsInChildren(typeof(MeshFilter));
			for (int i=0;i<filters.Length;i++) {
				filters[i].renderer.enabled = false;
			}

			GUI.Box (new Rect (10,10, Screen.width / 2 - 20,Screen.height - 20), "Game Over !");
			
			if (GUI.Button (new Rect (30,50,Screen.width / 2 - 60,60), "Restart game")) {
				Application.LoadLevel("scene2");
				Time.timeScale = 1.0f;
				GameObject.Destroy( gameObject );
			}
			
			if (GUI.Button (new Rect (30,120,Screen.width / 2 - 60,60), "Exit game")) {
				Application.Quit();
			}
		}
	}
}
