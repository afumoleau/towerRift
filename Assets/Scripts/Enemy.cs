using UnityEngine;
using System.Collections;

/// <summary>
///  Class "Enemy" manages interactions enemies (Attacks crystal and travels on the map)
/// </summary>
public class Enemy : MonoBehaviour
{
	private float clock = 0f;
	public const float reloadTime = 0.5f;
	public const float range = 10f;
	public int hitPoints = 5;
	public int maxHealth = 5;
	public int curHealth = 5;
	public float healthBarLength;
	public Transform bulletPrefab;

	private CrystalHealth crystal;
	private int currentWayPoint;
	private int tmpDestinationWayPoint;
	private int destinationWayPoint;
	private int[] way;
	private bool changeWay = false;
	private int index = 0;

	/// <summary>
	/// Function that allows to retrieve the Crystal component.
	/// </summary>
	void Start ()
	{
		crystal = (CrystalHealth)GameObject.Find("Crystal").GetComponent("CrystalHealth");
	}

	/// <summary>
	/// Function that allows to manage the behavior of enemies. If the enemies are near the crystal then they will be shooting.
	/// </summary>
	void Update ()
	{
		clock += Time.deltaTime;
		if(clock > reloadTime)
		{
			if(Vector3.Distance(transform.position, crystal.transform.position) <= range)
			{
				NavMeshAgent nma = GetComponent<NavMeshAgent>();
				nma.enabled = false;
				Transform newBullet = Instantiate(bulletPrefab) as Transform;
				newBullet.parent = transform.parent;
				newBullet.position = transform.position;
				newBullet.GetComponent<Bullet>().source = transform;
				newBullet.GetComponent<Bullet>().destination = crystal.transform;
				clock = 0f;
			}

		}

		if (changeWay == true) {
			currentWayPoint = way[index];
			destinationWayPoint = way[index + 1];
			index += 1;
			
			this.GetComponent<NavMeshAgent> ().SetDestination (EnemyManager.getWayTransform (destinationWayPoint).position);
			changeWay = false;
		} else if (Mathf.Abs (this.GetComponent<NavMeshAgent> ().nextPosition.x - (EnemyManager.getWayTransform (destinationWayPoint)).position.x) < 5
		           && Mathf.Abs(this.GetComponent<NavMeshAgent> ().nextPosition.z - (EnemyManager.getWayTransform (destinationWayPoint)).position.z) < 5
		           && destinationWayPoint != 65) {
			changeWay = true;
		}
	}

	/// <summary>
	/// Function that calculates a random path to the enemy waves.
	/// </summary>
	/// <param name=wayRandom>The random way to initialize.</param>
	public void initializeWay(int[] wayRandom) {
		way = new int[wayRandom.Length];
		for (int i = 0; i < wayRandom.Length; i++) {
			way[i] = wayRandom[i];
		}
		changeWay = true;
	}

	/// <summary>
	/// 
	/// </summary>
	public void hit()
	{
		hitPoints--;
		healthBarLength = healthBarLength * (curHealth /(float)maxHealth);
		if(hitPoints <= 0){
			GameObject player = GameObject.Find("Player");
			if( player ){
				PutBase playerBase = ((PutBase)player.GetComponent ("PutBase"));
				if( playerBase ){
					playerBase.putMoney (100);
				}
			}
			Destroy(gameObject);
		}
	}
}
