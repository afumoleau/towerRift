using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
	private Transform player;
	//adapt mouse movement with camera movement in 3D.
	public float sensitivity=5f;
	private float xRotation;
	private float yRotation;
	private float currentXRotation;
	private float currentYRotation;
	private float xRotationV;
	private float yRotationV;
	public float lookSmoothDamp=.1f;

	void Start()
	{
		player = transform.parent;
		switch(PlayerPrefs.GetInt("gameMode"))
		{
			case 0 : // Only standard player
				GetComponent<Camera>().rect = new Rect(0f,0f,1f,1f);
				break;
			case 1 : // Only Rift player
				gameObject.SetActive(false);
				break;
			default : // Both players
				GetComponent<Camera>().rect = new Rect(0f,0f,0.5f,1f);
				break;
		}
		Screen.showCursor = false;
	}

	void FixedUpdate() 
	{
		xRotation -= Input.GetAxis ("Mouse Y") * sensitivity;
		yRotation += Input.GetAxis ("Mouse X") * sensitivity;

		xRotation = Mathf.Clamp (xRotation, -90, 90);

		currentXRotation = Mathf.SmoothDamp (currentXRotation, xRotation,ref xRotationV, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp (currentYRotation, yRotation,ref yRotationV, lookSmoothDamp);

		transform.rotation = Quaternion.Euler (currentXRotation, currentYRotation, 0);
		player.transform.rotation = Quaternion.Euler(0.0f, currentYRotation, 0.0f);

	}
}
