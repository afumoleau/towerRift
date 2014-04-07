using UnityEngine;
using System.Collections;

public class TowerCubeManipulationTest : MonoBehaviour
{
	public Transform transformToMove;
	private float clock = 0f;
	public float duration = 5f;

	void Start ()
	{
	}
	
	void Update ()
	{
		clock += Time.deltaTime;
		transformToMove.position = Vector3.Lerp(new Vector3(-5f, 7.5f, 0f), new Vector3(5f, 7.5f, 0f), clock / duration);
	}
}
