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

	private float myWidth;
	private float myHeight;
	private Vector3 middleScreen;

	private double rate;


	void Start() {
		myWidth = (float)(Screen.width * 0.25);
		myHeight = (float)(Screen.height * 0.5);
	 	middleScreen = new Vector3 (myWidth, myHeight, 0f);
	}


	// Update is called once per frame
	void Update () {

		lineTransform = transform.position;
		startLine = transform.position;
		RaycastHit hit;

		Ray cam = c.ScreenPointToRay (middleScreen);
		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast (cam, out hit, 1000)) {
				if(hit.transform.tag == "Enemy"){
					Transform particleClone = Instantiate(effect,hit.point,Quaternion.LookRotation(hit.normal)) as Transform;
					StartCoroutine("impact",particleClone);
					//Send the damage at the method ApplyDamage within enemy Script
					hit.transform.GetComponent<EnemyHealth>().applyDamage(25f);
					lineTransform = hit.point;
				}
				if(hit.transform.tag == "towerCube"){
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
	IEnumerator impact (Transform thisTransform) {
		yield return new WaitForSeconds(1.0f);
		GameObject.Destroy(thisTransform.gameObject);
	}

}
