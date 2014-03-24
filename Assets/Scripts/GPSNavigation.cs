using UnityEngine;
using System.Collections;

public class GPSNavigation : MonoBehaviour {
	public Vector3 target;
	public bool hasChanged;
	public Material mat;

	private NavMeshPath path;
	private Vector2 center;
	private bool pathActive;

	private const float radius = 10.0f;

	// Use this for initialization
	void Start () {
		path = new NavMeshPath();
		LineRenderer LR = gameObject.GetComponent<LineRenderer>();
		/*LR.material = mat;
		LR.SetColors(Color.blue, Color.blue);*/

		LR.useWorldSpace = true;
		//LR.enabled = true;

		center.x = transform.position.x;
		center.y = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if( pathActive ){
			Vector2 pos;
			pos.x = transform.position.x;
			pos.y = transform.position.z;
			bool outsideCircle = ((pos.x - center.x)*(pos.x - center.x)) + ((pos.y - center.y)*(pos.y - center.y)) > radius * radius;
			bool arrived = (((pos.x - target.x)*(pos.x - target.x)) + ((pos.y - target.z)*(pos.y - target.z))) < 25.0f;

			// If the player move more than 10 unit away from his start position we recalculate the path
			if( outsideCircle ){
				hasChanged = true;
				center.x = transform.position.x;
				center.y = transform.position.z;
			}

			if( arrived ){
				LineRenderer LR = GetComponent<LineRenderer>();
				LR.enabled = false;
				hasChanged = false;
				pathActive = false;
				Debug.Log (" I'M ARRIVED !!");
			}
			
		}

		if( hasChanged ){
			bool res = NavMesh.CalculatePath(transform.position, target, -1, path);
			if(res) {
				Debug.Log ("PATH FOUND");
				Debug.Log ("Path length = " + path.corners.Length);
				DrawPath ( transform.position, path.corners );
				pathActive = true;
			} else {
				Debug.Log ("PATH NOT FOUND");
				pathActive = false;
			}
			hasChanged = false;
		}


	}

	void DrawPath( Vector3 playerPosition, Vector3[] pointList ){
		LineRenderer LR = GetComponent<LineRenderer>();
		LR.enabled = true;
		LR.SetWidth(0.2f, 0.2f);
		LR.SetVertexCount(pointList.Length);

		for( int i = 0; i < pointList.Length; ++i ){
			Vector3 pointInPath = pointList[i];
			LR.SetPosition(i, new Vector3(pointInPath.x, -2.8f, pointInPath.z));
		}
	}
}
