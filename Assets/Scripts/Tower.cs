using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
	public float reloadTime = 0.5f;
	public float range = 3f;
	public Transform bulletPrefab;

	private float clock = 0f;

	void Start ()
	{
	}
	
	void Update ()
	{
		clock += Time.deltaTime;
		if(clock > reloadTime)
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject enemy in enemies)
			{
				if(Vector3.Distance(transform.position, enemy.transform.position) <= range)
				{
					Transform newBullet = Instantiate(bulletPrefab) as Transform;
					newBullet.parent = transform.parent;
					newBullet.position = transform.position;
					newBullet.GetComponent<Bullet>().source = transform;
					newBullet.GetComponent<Bullet>().destination = enemy.transform;
					clock = 0f;
					break;
				}
			}
		}
	}
}
