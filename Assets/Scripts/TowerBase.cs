using UnityEngine;
using System.Collections;

public class TowerBase : MonoBehaviour
{
	public int cubesCount = 0;
	public int cubesNeeded = 3;
	public Transform evolveInto;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter(Collision collider)
	{
		if(collider.gameObject.CompareTag("towerCube"))
		{
			Destroy(collider.gameObject);
			collider.transform.parent = transform;
			cubesCount++;
		}
	}

	public void addCube(Transform towerCube)
	{
		towerCube.parent = transform;
		towerCube.localPosition = new Vector3(0,0,0) + new Vector3(0f, 0f, -5f * cubesCount);
		cubesCount++;
		if(cubesCount == cubesNeeded)
		{
			Transform newTower = Instantiate(evolveInto) as Transform;
			newTower.transform.position = this.transform.position;
			Destroy(this.gameObject);
		}
	}
}
