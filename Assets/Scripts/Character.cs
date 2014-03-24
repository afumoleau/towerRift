using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Character : MonoBehaviour
{
	public int wiimoteConnected = 0;
	public float forwardSpeed = 100f;
	public float turnSpeed = 0.05f;
	public float jumpForce = 350f;
	public TowerManager towerManager;
	public GameObject menuInGame;
	public AnimationClip menuEnter;

	private float jump = 0f;
	private bool stop = false;

	void Start()
	{
		animation.AddClip(menuEnter, "menuEnter");
	}
	
	void Update()
	{
		KeyboardMove();
		Action();
	}

	void KeyboardMove()
	{
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 moveVector = Vector3.zero;

		if (Input.GetKey(KeyCode.Q))
        	transform.Rotate(-Vector3.up * turnSpeed);
		else if (Input.GetKey(KeyCode.D))
        	transform.Rotate(Vector3.up * turnSpeed);
		if (Input.GetKey(KeyCode.Z))
			moveVector = transform.forward * forwardSpeed;
		else if (Input.GetKey(KeyCode.S))
			moveVector = -transform.forward * forwardSpeed;
	
		if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
			jump = jumpForce;
		if (jump > 0f)
		{
			moveVector.y = jump;
			jump += Physics.gravity.y / 5;
		}
		moveVector += Physics.gravity;

		controller.Move(moveVector * Time.deltaTime);
	}

	void Action ()
	{
		if (Input.GetKeyDown (KeyCode.A))
			towerManager.spawn (transform.position + transform.forward * 2);

		if (Input.GetKeyDown (KeyCode.Escape))
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
	}
}
