using UnityEngine;
using System.Collections;

/// <summary>
///  Manages the hints given by the Oculus player
/// </summary>
public class HintManager : Singleton<HintManager>
{
	protected HintManager(){}

	public Transform spawn;
	private NavMeshPath navMeshPath;
	public Vector3 pathStart;
	public Vector3 pathEnd;
	private const float radius = 10.0f;

	/// <summary>
	/// Initializes the references to components
	/// </summary>
	void Start ()
	{
		navMeshPath = new NavMeshPath();
		LineRenderer line = GetComponent<LineRenderer>();
		line.enabled = false;
	}
	
	/// <summary>
	/// Called each frame and refresh the path hint if needed
	/// </summary>
	void Update ()
	{
		LineRenderer line = GetComponent<LineRenderer>();
		if(line.enabled)
		{
			Vector3 characterPosition = Character.Instance.transform.position;

			// Character moved from the starting point 
			if (Vector3.Distance(pathStart, characterPosition) >= radius)
			{
				pathStart = characterPosition;
				if(NavMesh.CalculatePath(pathStart, pathEnd, -1, navMeshPath))
					DrawPath(navMeshPath.corners);
			}

			// Character close to the destination
			if(Vector3.Distance(pathEnd, characterPosition) <= radius)
				line.enabled = false;	
		}
	}

	/// <summary>
	/// Creates a pin the game, hinting a specific position
	/// </summary>
	public void pin(Vector3 position)
	{
		Transform newPin = Instantiate(spawn) as Transform;
		newPin.position = position;
		newPin.parent = transform;

		pathStart = Character.Instance.transform.position;
		pathEnd = position;
	
		if(NavMesh.CalculatePath(pathStart, pathEnd, -1, navMeshPath)) 
			DrawPath(navMeshPath.corners);

		Destroy(newPin.gameObject, 1);
	}

	/// <summary>
	/// Draws a path hint
	/// </summary>
	/// <param name=points>An array of points defining the path to be drawn.</param>
	void DrawPath(Vector3[] points)
	{
		LineRenderer line = GetComponent<LineRenderer>();
		line.enabled = true;
		line.SetVertexCount(points.Length);
		for (int i = 0; i < points.Length; ++i)
			line.SetPosition(i, new Vector3(points[i].x, points[i].y, points[i].z));
	}
}