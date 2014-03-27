using UnityEngine;
using System.Collections;

public class ShotPlayer : MonoBehaviour {

	public float damage = 50f;
	public Transform effect;
	private Vector3 lineTransform ;
	private Vector3 startLine;
	public Camera c;
	private Vector3 startPos = Vector3.zero;
	private Vector3 endPos = Vector3.zero;
	private const int speed = 5;

	double rate;
	void Start() {
	}


	// Update is called once per frame
	void Update () {

		lineTransform = transform.position;
		startLine = transform.position;
		float myWidth = (float)(Screen.width * 0.25);
		float myHeight = (float)(Screen.height * 0.5);

		Vector3 middleScreen = new Vector3 (myWidth, myHeight, 0f);
		RaycastHit hit;

		//Ray camera2 = camera.ScreenPointToRay (middleScreen);
		Ray cam = Camera.main.ScreenPointToRay (middleScreen);
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log ("Heho");
			if (Physics.Raycast (cam, out hit, 1000)) {
				Object particleClone = Instantiate(effect,hit.point,Quaternion.LookRotation(hit.normal));
				Destroy(((Transform)particleClone).gameObject);

				//Send the damage at the method ApplyDamage wich is in enemy Script
				hit.transform.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				lineTransform = hit.point;

				if(hit.transform.tag == "cubeTower"){
					//StartPos, endPos to lerp cubeTower near to the player
					startPos = hit.transform.position;
					endPos = transform.position + new Vector3(0.0f,2.0f,2.0f);

					//Call coroutine function to move our hit transform from startLerp to endLerp
					StartCoroutine("MoveObject", hit.transform);
				}
			}
		}
		Debug.DrawRay (startLine, lineTransform, Color.red);
		Debug.DrawRay (transform.position,Vector3.forward, Color.blue);
	}

	//Give Transform you want to 
	IEnumerator MoveObject (Transform thisTransform) {
		double i= 0.0;
		rate = 0.5;
		while (i < 1.0) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, ((float) i)* speed); 
			yield return new WaitForEndOfFrame();
		}
	}
}
