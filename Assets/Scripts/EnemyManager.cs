using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public Transform source;
	public Transform destination;
	public float spawnDelay;
	public int leftToSpawn;
	public Transform spawn;

	private float clock = 0f;

	void Start ()
	{
	}
	
	void Update ()
	{
		clock += Time.deltaTime;
		if (clock > spawnDelay && leftToSpawn > 0) {
			Transform newSpawn = Instantiate (spawn) as Transform;
			newSpawn.parent = transform;
			newSpawn.position = source.position;
			newSpawn.GetComponent<NavMeshAgent> ().SetDestination (destination.position);

			leftToSpawn--;
			clock = 0;
		}

	}
}
