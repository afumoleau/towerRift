using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public Transform target;
	private NavMeshAgent agent;

	void Start ()
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}
	
	void Update ()
	{
	}
}
