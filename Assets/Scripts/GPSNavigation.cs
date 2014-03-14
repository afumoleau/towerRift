using UnityEngine;
using System.Collections;

public class GPSNavigation : MonoBehaviour {
	public Transform target;
	private NavMeshPath path;
	public bool hasChanged;
	public LineRenderer LR;
	// Use this for initialization
	void Start () {
		path = new NavMeshPath();
	}
	
	// Update is called once per frame
	void Update () {
		if( hasChanged ){
			bool res = NavMesh.CalculatePath(transform.position, target.position, -1, path);
			if(res) {
				Debug.Log ("PATH FOUND");
				Debug.Log ("Path length = " + path.corners.Length);
				DrawPath ( transform.position, path.corners );
			} else {
				Debug.Log ("PATH NOT FOUND");
			}
			hasChanged = false;
			LR.enabled = false;
		}
	}

	void DrawPath( Vector3 playerPosition, Vector3[] pointList ){
		LR.SetVertexCount(pointList.Length);

		for( int i = 0; i < pointList.Length; ++i ){
			Vector3 pointInPath = pointList[i];
			LR.SetPosition(i, new Vector3(pointInPath.x, -2.8f, pointInPath.z));
		}
	}
}
