using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int hitPoints = 5;

	public int maxHealth = 5;
	public int curHealth = 5;
	
	public float healthBarLength;

	void Start ()
	{
	}
	
	void Update ()
	{ 
	}

	public void hit()
	{
		hitPoints--;
		healthBarLength = healthBarLength * (curHealth /(float)maxHealth);
		if(hitPoints <= 0)
			Destroy(gameObject);
	}
	/*
	void OnGUI() {
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		GUI.Box(new Rect(targetPos.x - healthBarLength / 2, Screen.height - targetPos.y - 10, 20, 5), "");
	}*/
}
