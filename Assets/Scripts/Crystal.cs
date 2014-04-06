using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour 
{

	public float health;

	private const float maxHealth = 1000.0f;
	private bool stop = false;
	public AudioClip gameOverClip;

	void Start () 
	{
		health = 100f;
	}

	void Update()
	{
	}

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

	IEnumerator endGame()	
	{
		audio.PlayOneShot(gameOverClip);
		yield return new WaitForSeconds(gameOverClip.length);
		Time.timeScale = 1;
		Application.LoadLevel("Game");
	}
}

/*
using UnityEngine;
using System.Collections;

public class BarCrystalHealth : MonoBehaviour {

	Vector2 pos=new Vector2(20,40);
	Vector2 size=new Vector2(80,20);
	
	public Texture2D supBarHealth;
	public Texture2D barHealth;

	private float lifeLeft;

	void Update () {
		lifeLeft = GameObject.Find("Crystal").GetComponent<health>().health ;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(10, 20, barHealth.width * lifeLeft / 100, barHealth.height), barHealth);
		GUI.DrawTexture(new Rect(10, 20, supBarHealth.width, barHealth.height), supBarHealth);
	}
}
*/