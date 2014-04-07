using UnityEngine;
using System.Collections;

/// <summary>
///  Manages the towers
/// </summary>
public class TowerManager : Singleton<TowerManager>
{
	protected TowerManager(){}

	public Transform spawnPrefab;
	public Transform towerBasePrefab;
	public Transform towerCubePrefab;

	public float towerCubeSpawnRange = 25f;

	void Start()
	{
	}
	
	void Update()
	{
	}

	/// <summary>
	/// Add a tower in the game
	/// </summary>
	/// <param name=position>The position of the tower to be created (Y coordinates is ignored).</param>
	public void addTower(Vector3 position)
	{
		Transform newTower = Instantiate(spawnPrefab) as Transform;
		newTower.parent = transform;
		newTower.position = new Vector3(position.x, newTower.position.y, position.z);
	}

	/// <summary>
	/// Add a tower base in the game
	/// </summary>
	/// <param name=position>The position of the tower base to be created (Y coordinates is ignored).</param>
	public void addTowerBase(Vector3 position)
	{
		Transform newTowerBase = Instantiate(towerBasePrefab) as Transform;
		newTowerBase.parent = transform;
		newTowerBase.position = new Vector3(position.x, newTowerBase.position.y, position.z);

		for(int i = 0; i < 3; ++i)
			spawnTowerCube();
	}

	/// <summary>
	/// Add a tower cube on the map at a random position
	/// </summary>
	public void spawnTowerCube()
	{
		Transform newTowerCube = Instantiate(towerCubePrefab) as Transform;
		newTowerCube.parent = transform;
		newTowerCube.position = new Vector3(-towerCubeSpawnRange + Random.value * 3 * towerCubeSpawnRange, towerCubePrefab.position.y, -towerCubeSpawnRange + Random.value * 3 * towerCubeSpawnRange);
	}
}
