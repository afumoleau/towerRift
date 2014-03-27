using UnityEngine;
using System.Collections;

public class selectCubeForBuild : MonoBehaviour {

	public GameObject elbow_right;
	public GameObject hand_right;
	public LineRenderer rayVisible;

	private Vector3 lineTransform;
	private Vector3 startLine;
	private GameObject player;
	private FixedJoint playerFix;

	void Start(){
		//player = GameObject.Find ("hand_right");
	}
	
	void Update () {
		Vector3 startLine = elbow_right.transform.position;
		Vector3 endLine = hand_right.transform.position;
		Vector3 direction = (endLine - startLine) * 15;
		
		RaycastHit hit;
		Ray ray = new Ray (startLine, direction);
		
		if (Physics.Raycast (ray, out hit, 1000)) {
			if(hit.transform.tag == "cubeTower"){
				Debug.Log ("cube Touché par kinect");
				//Transform player2 = player.GetComponent<Transform>();
				hit.transform.parent = hand_right.transform;
				lineTransform = hit.point;
			}
		}

		rayVisible.SetPosition(0, startLine);
		//endLine = direction * 15;

		rayVisible.SetPosition(1, startLine+direction);

		Debug.DrawRay(startLine, direction, Color.red);
	}
}