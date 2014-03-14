using UnityEngine;
using System.Collections;

public class PinManager : MonoBehaviour
{
	public Transform spawn;

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	public void pin(Vector3 position)
	{
		Transform newPin = Instantiate(spawn) as Transform;
		newPin.position = position;
		newPin.parent = transform;

		GameObject nav = GameObject.Find("Cube");
		GPSNavigation c = (GPSNavigation)nav.GetComponent("GPSNavigation");
		c.target = newPin.transform; 
		c.hasChanged = true;

		Destroy(newPin.gameObject, 1);
	}
}
