using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Character : MonoBehaviour
{
	public float speed = 1f;
	public float jumpForce = 350f;
	public TowerManager towerManager;
	public GameObject menuInGame;

	bool stop = false;
	Camera camera;

	CharacterController controller;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		camera = transform.Find("Camera").GetComponent<Camera>();
	}
	
	void Update()
	{
		KeyboardMove();

		if(Input.GetKeyDown(KeyCode.A))
			spawnTowerBase();
	}

	void KeyboardMove()
	{
		Vector3 moveVector = Vector3.zero;
		if (Input.GetKey(KeyCode.Z)) moveVector += transform.forward;
		if (Input.GetKey(KeyCode.S)) moveVector += -transform.forward;
		if (Input.GetKey(KeyCode.D)) moveVector += transform.right;
		if (Input.GetKey(KeyCode.Q)) moveVector += -transform.right;
		controller.Move(moveVector.normalized * speed * Time.deltaTime);
	}

	void spawnTowerBase()
	{
		// Cast a ray from the center of the viewport
		Ray cameraRay = camera.ScreenPointToRay(new Vector3((camera.rect.x + camera.rect.width / 2f) * Screen.width, (camera.rect.y + camera.rect.height / 2f) * Screen.height, 0f));
		RaycastHit hit;
		if (Physics.Raycast(cameraRay, out hit, 100f) && (hit.collider.gameObject.tag == "Grid"))
		{
			// Add tower base at hit position
			Vector3 positionHit = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			towerManager.addTowerBase(positionHit);
		}
	}
}
