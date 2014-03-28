using UnityEngine;
using System.Collections;

public class selectCubeForBuild : MonoBehaviour {

	private Transform player;
	public Camera c;
	private Vector3 lineTransform;
	private Vector3 startLine;

	/*Variable For RayCast */
	private float myWidth;
	private float myHeight;
	private Vector3 middleScreen;
	
	void Start(){
		//player = GameObject.Find ("hand_right");
		myWidth = (float)(Screen.width * 0.25);
		myHeight = (float)(Screen.height * 0.5);
		middleScreen = new Vector3 (myWidth, myHeight, 0f);
	}

	void Update () {
		RaycastHit hit;
		Ray cam = c.ScreenPointToRay (middleScreen);
		if(Input.GetKeyDown(KeyCode.E)){
			if (Physics.Raycast (cam, out hit, 1000)) {
				if(hit.transform.tag == "towerCube"){
					hit.transform.parent = GameObject.Find("Player2").GetComponent<Transform>();
				}
			}
		}
	}
}