using UnityEngine;
using System.Collections;

public class ShotPlayer : MonoBehaviour {

	public float damage = 50f;
	public Transform effect;
	private Vector3 lineTransform ;
	private Vector3 startLine;


	// Update is called once per frame
	void Update () {
		
		lineTransform = transform.position;
		startLine = transform.position;
		float myWidth = (float)(Screen.width * 0.5);
		float myHeight = (float)(Screen.height * 0.5);

		Vector3 middleScreen = new Vector3 (myWidth, myHeight, 0f);
		RaycastHit hit;
		Ray rayPlayer = new Ray (transform.position, Vector3.forward);
		Ray camera = Camera.main.ScreenPointToRay (middleScreen);

		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast (camera, out hit, 1000)) {

				Object particleClone = Instantiate(effect,hit.point,Quaternion.LookRotation(hit.normal));
				Destroy(particleClone);
				hit.transform.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				lineTransform = hit.point;
			}
		}
		Debug.DrawRay (startLine, lineTransform, Color.red);
		Debug.DrawRay (transform.position,Vector3.forward, Color.blue);
	}
}
