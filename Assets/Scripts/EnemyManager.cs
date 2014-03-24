using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public Transform source1;
	public Transform source2;
	public Transform source3;
	public Transform source4;
	public Transform destination;
	public float spawnDelay;
	public int leftToSpawn;
	public Transform spawn;

	private Transform source;
	private float clock = 0f;

	void Start ()
	{
		spawnRandom ();
	}
	
	void Update ()
	{
		clock += Time.deltaTime;
		if (clock > spawnDelay)
		{
			if (leftToSpawn > 0)
			{
				Transform newSpawn = Instantiate (spawn) as Transform;
				newSpawn.parent = transform;
				newSpawn.position = source.position + new Vector3(0f,0.5f,0f);
				newSpawn.GetComponent<NavMeshAgent> ().SetDestination (new Vector3(destination.position.x, 0.5f, destination.position.z));
				leftToSpawn--;
				clock = 0;

				if (leftToSpawn == 0)
				{ // A vérifier avec plusieurs vagues d'ennemies.
					spawnRandom();
				}
			}
		}

	}

	void spawnRandom () {
		int random = Random.Range (0, 4);
		switch (random) { 
		case 0:
			source = source1;
			break;
		case 1: 
			source = source2;
			break;
		case 2: 
			source = source3;
			break;
		case 3: 
			source = source4; 
			break;
		}
	}
}
