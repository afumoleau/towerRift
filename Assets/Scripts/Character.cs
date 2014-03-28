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
	public AnimationClip menuEnter;

	bool stop = false;
	Camera camera;

	CharacterController controller;

	void Start()
	{
		//animation.AddClip(menuEnter, "menuEnter");
		controller = GetComponent<CharacterController>();
		camera = transform.Find("Camera").GetComponent<Camera>();
	}
	
	void Update()
	{
		KeyboardMove();
		Action();
	}

	void KeyboardMove()
	{
		Vector3 moveVector = Vector3.zero;
		if (Input.GetKey(KeyCode.Z)) moveVector += transform.forward;
		if (Input.GetKey(KeyCode.S)) moveVector += -transform.forward;
		if (Input.GetKey(KeyCode.D)) moveVector += transform.right;
		if (Input.GetKey(KeyCode.Q)) moveVector += -transform.right;
	/*
		if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
			jump = jumpForce;
		if (jump > 0f)
		{
			moveVector.y = jump;
			jump += Physics.gravity.y / 5;
		}
		moveVector += Physics.gravity;
*/
		controller.Move(moveVector.normalized * speed * Time.deltaTime);
	}

	void Action ()
	{
		// if (Input.GetKeyDown (KeyCode.A))
			// towerManager.spawn (transform.position + transform.forward * 2);

		if (Input.GetKey (KeyCode.Escape))
		{
			if (stop)
			{
				Time.timeScale = 1;
				menuInGame.SetActive(false);
				stop = false;
			}
			else
			{
				Time.timeScale = 0;
				menuInGame.SetActive(true);
				stop = true;
				//animation.Play();
			}
		}

		if(Input.GetKey(KeyCode.A))
		{
			// Cast a ray from the center of the viewport
			Ray cameraRay = camera.ScreenPointToRay(new Vector3((camera.rect.x + camera.rect.width / 2f) * Screen.width, (camera.rect.y + camera.rect.height / 2f) * Screen.height, 0f));
			RaycastHit hit;
			if (Physics.Raycast(cameraRay, out hit, 100f) && (hit.collider.gameObject.tag == "Grid"))
			{
				// Retrieve hit position
				Vector3 positionHit = new Vector3(hit.point.x, hit.point.y, hit.point.z);

				// Add tower base
				towerManager.addTowerBase(positionHit);
			}
		}
	}
}
