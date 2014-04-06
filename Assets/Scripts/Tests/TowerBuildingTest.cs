using UnityEngine;
using System.Collections;

public class TowerBuildingTest : MonoBehaviour
{
	public Transform[] towerCubes;
	public Transform[] positions;
	float time = 0f;
	float duration = 5f;

	
	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		for(int i = 0; i < towerCubes.Length; ++i)
		{
			if(towerCubes[i] != null)
				towerCubes[i].position = Vector3.Lerp(towerCubes[i].position, new Vector3(0,0,0), time / (duration * (i+1)));
		}
	}
}
