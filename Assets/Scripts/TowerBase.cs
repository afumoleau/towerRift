using UnityEngine;
using System.Collections;

/// <summary>
///  Manages the behaviour of tower bases
/// </summary>
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

	/// <summary>
	/// Called when an object enter the trigger
	/// </summary>
	/// <param name=collider>The collider which entered the trigger.</param>
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.CompareTag("towerCube"))
			addCube(collider.transform);
	}

	/// <summary>
	/// Add a cube to the tower base
	/// </summary>
	/// <param name=towerCube>The cube to add.</param>
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
