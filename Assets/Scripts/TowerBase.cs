using UnityEngine;
using System.Collections;

public class TowerBase : MonoBehaviour
{
	public int cubesCount = 0;
	public int cubesNeeded = 3;
	public Transform evolveInto;

	public Color[] cubeColors = new Color[3]{Color.yellow, Color.green, Color.cyan};

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.CompareTag("towerCube"))
			addCube(collider.transform);
	}

	public void addCube(Transform towerCube)
	{
		towerCube.parent = transform;
		towerCube.localPosition = new Vector3(0,0,0) + new Vector3(0f, 0f, -5f * cubesCount); //FIXME
		towerCube.renderer.material.color = cubeColors[cubesCount%cubeColors.Length];
		towerCube.gameObject.tag = "Untagged";

		cubesCount++;
		if(cubesCount == cubesNeeded)
		{
			Transform newTower = Instantiate(evolveInto) as Transform;
			newTower.transform.position = this.transform.position;
			Destroy(this.gameObject);
		}
	}
}
