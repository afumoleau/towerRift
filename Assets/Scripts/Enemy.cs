using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the behaviour of enemies.
/// </summary>
public class Enemy : MonoBehaviour
{
	private float clock = 0f;
	public float reloadTime = 0.5f;
	public float range = 10f;
	public int maxHealth = 5;
	public int curHealth = 5;
	public float healthBarLength;
	public Transform bulletPrefab;
	public TextMesh healthText;

	private Crystal crystal;
	private int currentWayPoint;
	private int tmpDestinationWayPoint;
	private int destinationWayPoint;
	private int[] way;
	private bool changeWay = false;
	private int index = 0;

	private Character character;

	/// <summary>
	/// Initializes referenced objects.
	/// </summary>
	void Start ()
	{
		character = Character.Instance;
		crystal = GameObject.Find("crystal").GetComponent<Crystal>();
		healthText = transform.Find("healthText").GetComponent<TextMesh>();
		healthText.text = this.curHealth.ToString()+ "/"+ maxHealth;
	}

	/// <summary>
	/// Called each frame. Shoot at the crystal when in range
	/// </summary>
	void Update ()
	{
		clock += Time.deltaTime;
		if(clock > reloadTime)
		{
			if(Vector3.Distance(transform.position, crystal.transform.position) <= range)
			{
				// Stop moving
				NavMeshAgent nma = GetComponent<NavMeshAgent>();
				nma.enabled = false;

				// Create a new bullet
				Transform newBullet = Instantiate(bulletPrefab) as Transform;
				newBullet.parent = transform.parent;
				newBullet.position = transform.position;
				newBullet.GetComponent<Bullet>().source = transform;
				newBullet.GetComponent<Bullet>().destination = crystal.transform;
				clock = 0f;
			}

		}

		if (changeWay == true)
		{
			currentWayPoint = way[index];
			destinationWayPoint = way[index + 1];
			index += 1;
			
			this.GetComponent<NavMeshAgent> ().SetDestination (EnemyManager.getWayTransform (destinationWayPoint).position);
			changeWay = false;
		}
		else if (EnemyManager.getWayTransform (destinationWayPoint) != null &&
					Mathf.Abs (this.GetComponent<NavMeshAgent> ().nextPosition.x - (EnemyManager.getWayTransform (destinationWayPoint)).position.x) < 5
		           && Mathf.Abs(this.GetComponent<NavMeshAgent> ().nextPosition.z - (EnemyManager.getWayTransform (destinationWayPoint)).position.z) < 5
		           && destinationWayPoint != 65)
		{
			changeWay = true;
		}
	}

	/// <summary>
	/// Set a random path.
	/// </summary>
	/// <param name=randomWay>The random way to set as enemy way.</param>
	public void initializeWay(int[] randomWay)
	{
		way = new int[randomWay.Length];
		for (int i = 0; i < randomWay.Length; i++)
		{
			way[i] = randomWay[i];
		}
		changeWay = true;
	}

	/// <summary>
	/// Called when the enemy is hit by a bullet
	/// </summary>
	public void hit()
	{
		curHealth--;
		if(curHealth <= 0)
		{
				this.curHealth = 0;
			character.money += 100f;
			Destroy(gameObject);
		}

		healthText.text = this.curHealth.ToString()+ "/"+ maxHealth;
	}
}
