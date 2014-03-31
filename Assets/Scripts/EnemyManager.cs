using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
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
	
	void Start ()
	{
		Debug.Log ("Start !");
		spawnRandom ();
		
		ways = new Way ();
		
		way = new int[maxOfWayPoint];
		
		GameObject pathGO = GameObject.Find ("Path");
		path = (Transform) pathGO.GetComponent("Transform");
		listOfComponents = new Transform[65];
		listOfComponents = path.GetComponentsInChildren (typeof(Transform));
	}
	
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
				enemy.initialize(way);
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
	
	private bool isInWay(int n) {
		for (int i = 0; i < maxOfWayPoint; ++i) {
			if (way [i] == n)
				return true;
		}
		return false;
	}
	
	public static Transform getWayTransform (int n){
		return listOfComponents[findIndexWayPoint(n)].transform;
	}
	
	private void displayWay () {
		Debug.Log ("--- Way ---");
		for (int i = 0; i < maxOfWayPoint; i++) {
			Debug.Log (way [i]);
		}
		Debug.Log (" ");
	}
	
	private static int findIndexWayPoint (int n) {
		string str = string.Concat("Waypoint", n.ToString());
		for (int i = 1; i < 66; ++i) {
			if (listOfComponents[i].name == str)
				return i;
		}
		Debug.Log ("Error : WayPoint not found");
		return -1;
	}
	
	private void displayWayTransform () {
		for (int i = 0; i < 66; ++i) {
			Debug.Log (listOfComponents[i].name + " " + listOfComponents[i].transform.position.x + " " + listOfComponents[i].transform.position.y + " " + listOfComponents[i].transform.position.z);
		}
	}
}
