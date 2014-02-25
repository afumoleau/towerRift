using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour
{
	public Transform spawnPrefab;

	void Start()
	{
	}
	
	void Update()
	{
	}

	public void spawn(Vector3 position)
	{
		Transform newTower = Instantiate(spawnPrefab) as Transform;
		newTower.parent = transform.parent;
		newTower.position = new Vector3(position.x, newTower.position.y, position.z);
	}
}
