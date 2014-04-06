using UnityEngine;
using System.Collections;

public class TowerManager : Singleton<TowerManager>
{
	protected TowerManager(){}

	public Transform spawnPrefab;
	public Transform towerBasePrefab;
	public Transform towerCubePrefab;

	void Start()
	{
	}
	
	void Update()
	{
	}

	public void addTower(Vector3 position)
	{
		Transform newTower = Instantiate(spawnPrefab) as Transform;
		newTower.parent = transform;
		newTower.position = new Vector3(position.x, newTower.position.y, position.z);
	}

	public void addTowerBase(Vector3 position)
	{
		Transform newTowerBase = Instantiate(towerBasePrefab) as Transform;
		newTowerBase.parent = transform;
		newTowerBase.position = new Vector3(position.x, newTowerBase.position.y, position.z);

		for(int i = 0; i < 3; ++i)
			spawnTowerCube();
	}

	public void spawnTowerCube()
	{
		Transform newTowerCube = Instantiate(towerCubePrefab) as Transform;
		newTowerCube.parent = transform;
		newTowerCube.position = new Vector3(-25 + Random.value * 75, 0, -25 + Random.value * 75);
	}
}
