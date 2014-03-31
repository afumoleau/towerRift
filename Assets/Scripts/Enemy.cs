using UnityEngine;
using System.Collections;

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


	void Start ()
	{
		crystal = (CrystalHealth)GameObject.Find("Crystal").GetComponent("CrystalHealth");
	}
	
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
		} else if (Mathf.Abs (this.GetComponent<NavMeshAgent> ().nextPosition.x - (EnemyManager.getWayTransform (destinationWayPoint)).position.x) < 1 
		           && Mathf.Abs(this.GetComponent<NavMeshAgent> ().nextPosition.z - (EnemyManager.getWayTransform (destinationWayPoint)).position.z) < 1
		           && destinationWayPoint != 65) {
			changeWay = true;
		}
	}

	public void initialize(int[] wayRandom) {
		way = new int[wayRandom.Length];
		for (int i = 0; i < wayRandom.Length; i++) {
			way[i] = wayRandom[i];
		}
		changeWay = true;
	}


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
	/*
	void OnGUI() {
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		GUI.Box(new Rect(targetPos.x - healthBarLength / 2, Screen.height - targetPos.y - 10, 20, 5), "");
	}*/
}
