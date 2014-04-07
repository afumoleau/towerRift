using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour 
{
	public float health;
	public float maxHealth = 100f;
	private bool stop = false;
	public AudioClip gameOverClip;

	/// <summary>
	/// Initializes health
	/// </summary>
	void Start () 
	{
		health = maxHealth;
	}

	void Update()
	{
	}

	/// <summary>
	/// Called when the crystal is hit by a bullet
	/// </summary>
	public void hit(float damage)
	{
		health -= damage;
		if (health <= 0f)
		{
			health = 0f;
			if(!stop) 
			{
				StartCoroutine("endGame");
				stop = true;
			}
		}
	}

	/// <summary>
	/// Coroutine called when the crystal is destroyed, play a sound and reload level.
	/// </summary>
	IEnumerator endGame()	
	{
		audio.PlayOneShot(gameOverClip);
		yield return new WaitForSeconds(gameOverClip.length);
		Time.timeScale = 1;
		Application.LoadLevel("Game");
	}
}