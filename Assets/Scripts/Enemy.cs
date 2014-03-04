using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int hitPoints = 5;

	public int maxHealth = 5;
	public int curHealth = 100;
	
	public float healthBarLength;

	void Start ()
	{
		healthBarLength = Screen.width / 6;
	}
	
	void Update ()
	{
		AddjustCurrentHealth(0);   
	}

	public void hit()
	{
		hitPoints--;
		if(hitPoints <= 0)
			Destroy(gameObject);
	}

	void OnGUI() {
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 40, 30), curHealth + "/" + maxHealth);
	}

	public void AddjustCurrentHealth(int adj) {
		curHealth += adj;
	
		if (curHealth < 0)
			curHealth = 0;

		if (curHealth > maxHealth)
			curHealth = maxHealth;
		
		healthBarLength = (Screen.width / 6) * (curHealth /(float)maxHealth);
	}
}
