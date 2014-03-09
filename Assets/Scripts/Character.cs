using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Character : MonoBehaviour
{
	/*[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	[DllImport ("UniWii")]
	private static extern bool wiimote_isExpansionPortEnabled( int which );
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonA(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckC(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckZ(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickY(int which);*/

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
		/*wiimote_start();
		wiimoteConnected = wiimote_count();
		for (int i = 0; i < wiimoteConnected; ++i)
			if (!wiimote_isExpansionPortEnabled(i))
				Debug.Log("Wiimote #"+i+" has no nunchuk !");*/
	}
	
	void Update()
	{
		// Move character
		/*for (int i = 0; i < wiimoteConnected; ++i)
			NunchuckMove(i);
		if(wiimoteConnected <= 0)*/
			KeyboardMove();

		Action ();
	}

	/*void NunchuckMove(int wiimoteId)
	{
		if (wiimote_getButtonNunchuckC(wiimoteId))
			Debug.Log("C activate");
		if (wiimote_getButtonNunchuckZ(wiimoteId))
			rigidbody.AddForce(Vector3.up * jumpForce);
		if (wiimote_getNunchuckStickX(wiimoteId) <= 124 || wiimote_getNunchuckStickX(wiimoteId) >= 130)
			transform.Translate(Vector3.right * (wiimote_getNunchuckStickX(wiimoteId) - 127) / 300);
		if (wiimote_getNunchuckStickY(wiimoteId) <= 124 || wiimote_getNunchuckStickY(wiimoteId) >= 130)
			transform.Translate(Vector3.forward * (wiimote_getNunchuckStickY(wiimoteId) - 127) / 300);
	}*/

	void KeyboardMove()
	{
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 moveVector = Vector3.zero;

		if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        	transform.Rotate(-Vector3.up * turnSpeed);
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        	transform.Rotate(Vector3.up * turnSpeed);

		if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
			moveVector = transform.forward * forwardSpeed;
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			moveVector = -transform.forward * forwardSpeed;
	
		if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
			jump = jumpForce;
		if(jump > 0f)
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

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (stop) {
				Time.timeScale = 1;
				menuInGame.SetActive(false);
				stop = false;
			}
			else {
				Time.timeScale = 0;
				menuInGame.SetActive(true);
				stop = true;
				//animation.Play();

			}
		}
	}

	void OnApplicationQuit()
	{
		//wiimote_stop();
	}
}
