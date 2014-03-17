using UnityEngine;
using System.Collections;

public class MouseLookScript : MonoBehaviour {

	//our private player

	private GameObject player;
	//adapt mouse movement with camera movement in 3D.
	public float sensitivity=5f;
	private float xRotation;
	private float yRotation;
	private float currentXRotation;
	private float currentYRotation;
	private float xRotationV;
	private float yRotationV;
	public float lookSmoothDamp=.1f;

	void Start(){
		player = GameObject.Find("PlayerCube");
	}

		// Update is called once per frame
		
	void FixedUpdate() {
		xRotation -= Input.GetAxis ("Mouse Y") * sensitivity;
		yRotation += Input.GetAxis ("Mouse X") * sensitivity;

		xRotation = Mathf.Clamp (xRotation, -90, 90);

		currentXRotation = Mathf.SmoothDamp (currentXRotation, xRotation,ref xRotationV, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp (currentYRotation, yRotation,ref yRotationV, lookSmoothDamp);

		transform.rotation = Quaternion.Euler (currentXRotation, currentYRotation, 0);
		player = GameObject.Find ("Player");
		player.transform.rotation = Quaternion.Euler (0.0f, currentYRotation, 0.0f);

		}
	}
