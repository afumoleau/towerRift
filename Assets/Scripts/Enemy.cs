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
	private CrystalHealth crystal;
	public Transform bulletPrefab;

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
				Debug.Log ("ENEMY IN CRYSTAL RANGE !!!!!!");
				Transform newBullet = Instantiate(bulletPrefab) as Transform;
				newBullet.parent = transform.parent;
				newBullet.position = transform.position;
				newBullet.GetComponent<Bullet>().source = transform;
				newBullet.GetComponent<Bullet>().destination = crystal.transform;
				clock = 0f;
			}

		}
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
