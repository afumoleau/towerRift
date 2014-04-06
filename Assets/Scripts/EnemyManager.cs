using UnityEngine;
using System.Collections;

/// <summary>
///  Class "EnemyManager" initializes the waves enemies and their respective paths from random spawns.
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
	protected EnemyManager(){}
 
	public Transform source1;
	public Transform source2;
	public Transform source3;
	public Transform source4;
	public float spawnDelay;
	public float wavesDelay;
	public int maxLeftToSpawn;
	public Transform spawn;
	public static Way ways;
	public int maxOfWayPoint;
	public int nbWaves;
	
	private int sourceNb;
	private int leftToSpawn = 0;
	private int destination;
	private float clockSpawnDelay = 0f;
	private float clockWavesDelay = 0f;
	private Enemy enemy;
	private Transform source;
	private int[] way;
	private Transform path;
	private static Component[] listOfComponents;

	/// <summary>
	/// Initializes the attributes of the class at the beginning of the game.
	/// </summary>
	void Start ()
	{
		spawnRandom ();
		
		ways = new Way();
		
		way = new int[maxOfWayPoint];
		
		GameObject pathGO = GameObject.Find ("Path");
		if(pathGO != null)
		{
			path = (Transform) pathGO.GetComponent("Transform");
			listOfComponents = new Transform[65];
			listOfComponents = path.GetComponentsInChildren (typeof(Transform));
		}
	}

	/// <summary>
	/// Initializes the enemy waves.
	/// </summary>
	void Update ()
	{
		clockSpawnDelay += Time.deltaTime;
		clockWavesDelay += Time.deltaTime;

		if (clockSpawnDelay > spawnDelay) {
			if (leftToSpawn > 0) {
				Transform newSpawn = Instantiate (spawn) as Transform;
				newSpawn.parent = transform;
				newSpawn.position = source.position;
				enemy = newSpawn.GetComponent<Enemy> ();
				enemy.initializeWay(way);
				leftToSpawn--;
				if (leftToSpawn == 0)
					clockWavesDelay = 0;
				clockSpawnDelay = 0;
			}
			else if (leftToSpawn == 0 && nbWaves > 0) {
				if (clockWavesDelay > wavesDelay) {
					spawnRandom();
					wayRandom();
					leftToSpawn = maxLeftToSpawn;
					nbWaves -= 1;
				}
			}
		}
	}

	/// <summary>
	/// Computes a random spawn on the map.
	/// </summary>
	void spawnRandom () {
		int randomSource = Random.Range (0, 4);
		switch (randomSource) { 
		case 0:
			source = source1;
			sourceNb = 1;
			break;
		case 1: 
			source = source2;
			sourceNb = 4;
			break;
		case 2: 
			source = source3;
			sourceNb = 7;
			break;
		case 3:  
			source = source4;
			sourceNb = 10;
			break;
		}
	}

	/// <summary>
	/// Computes a random path on the map from a pre-calculated spawn.
	/// </summary>
	void wayRandom () {
		int point = sourceNb;
		int pointTmp;
		int rand;
		int i = 1;
		way [0] = point;
		do {
			pointTmp = point;
			do {
				rand = Random.Range(1, 3);
				pointTmp = ways.waysOfWayPoints[point - 1].tabNodes [rand];
			} while (pointTmp == 0 || (i > 2 ? (pointTmp == way[i-2]) : false));
			
			point = pointTmp;
			way[i] = point;
			if (point == 65)
				break;
			i++;
		} while (i != (maxOfWayPoint - 1));
		way [i] = 65; 
	}

	/// <summary>
	/// Determines if the checkpoint is not already on the way to boot. That permits to avoid that an enemy wave loop or turn back.
	/// </summary>
	/// <param name=n>The number of checkpoint.</param>
	/// <returns>Return "true" is the checkpoint is on the way to boot, "false" otherwise.</returns>
	private bool isInWay(int n) {
		for (int i = 0; i < maxOfWayPoint; ++i) {
			if (way [i] == n)
				return true;
		}
		return false;
	}

	/// <summary>
	/// Allows to find the checkpoint component corresponding to the number passed as a parameter.
	/// </summary>
	/// <param name=n>The number of the checkpoint.</param>
	/// <returns>The component which his number is passed as a parameter.</returns>
	public static Transform getWayTransform (int n){
		if(listOfComponents == null)
			return null;
		return listOfComponents[findIndexWayPoint(n)].transform;
	}

	/// <summary>
	/// Allows to display a way initialized. It is used for debugging.
	/// </summary>
	private void displayWay () {
		Debug.Log ("--- Way ---");
		for (int i = 0; i < maxOfWayPoint; i++) {
			Debug.Log (way [i]);
		}
		Debug.Log (" ");
	}

	/// <summary>
	/// Allows to retrieve the index in the table checkpoint whose number is passed as a parameter.
	/// </summary>
	/// <param name=n>The number of the checkpoint in the scene.</param>
	/// <returns>The index of the checkpoint in the table of checkpoints.</returns>
	private static int findIndexWayPoint (int n) {
		string str = string.Concat("Waypoint", n.ToString());
		if(listOfComponents != null)
		{
			for (int i = 1; i < 66; ++i) {
				if (listOfComponents[i].name == str)
					return i;
			}
		}
		Debug.Log ("Error : WayPoint not found");
		return -1;
	}

	/// <summary>
	/// Displays all checkpoints in the table of checkpoints with their position. It is used for debugging.
	/// </summary>
	private void displayWayTransform () {
		for (int i = 0; i < 66; ++i) {
			Debug.Log (listOfComponents[i].name + " " + listOfComponents[i].transform.position.x + " " + listOfComponents[i].transform.position.y + " " + listOfComponents[i].transform.position.z);
		}
	}
}
