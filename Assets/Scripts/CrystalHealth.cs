using UnityEngine;
using System.Collections;

public class CrystalHealth : MonoBehaviour {

	public float crystalHealth;

	private const float maxHealth = 1000.0f;
	private MenuPlayer menu;
	private bool stop = false;
	public AudioClip gameOverClip;

	void Start () {
		crystalHealth = 100f;
		menu = GameObject.Find("Camera").GetComponent<MenuPlayer>();
	}

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

	IEnumerator GameOverSound()
	{
		audio.PlayOneShot (gameOverClip);
		yield return new WaitForSeconds(gameOverClip.length);
		
		menu.RestartGame ();
	}


	private void GameOver(){
		StartCoroutine("GameOverSound");
	}
}
