using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int hitPoints = 5;

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	public void hit()
	{
		hitPoints--;
		if(hitPoints <= 0)
			Destroy(gameObject);
	}
}
