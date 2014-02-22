using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Displacement : MonoBehaviour {

	[DllImport ("UniWii")]
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
	private static extern byte wiimote_getNunchuckStickY(int which);

	float vitesse = 0.05f;

	// Use this for initialization
	void Start () {
		wiimote_start ();
	}
	
	// Update is called once per frame
	void Update () {
		int c = wiimote_count();
		if (c > 0) {	
			for (int i=0; i<=c-1; i++) {
				if (wiimote_isExpansionPortEnabled(i)) {
					NunchuckDisplacement(i);
				}
				else {
					Debug.Log ("Nunchuck is not connected to your wiimote !");
				}
			}
		}
		else {
			Debug.Log("Wiimote is not connected to your machine ! Please use keyboard or connect it.");
			KeyboardDisplacement();
		}
	}

	void NunchuckDisplacement (int i) {
		if (wiimote_getButtonNunchuckC(i)) {
			Debug.Log ("C activate");
		}
		if (wiimote_getButtonNunchuckZ(i)) {
			rigidbody.AddForce(Vector3.up * 350);
		}
		if (wiimote_getNunchuckStickX(i) <= 124 || wiimote_getNunchuckStickX(i) >= 130) {
			transform.Translate(Vector3.right * (wiimote_getNunchuckStickX(i) - 127) / 300);
		}
		if (wiimote_getNunchuckStickY(i) <= 124 || wiimote_getNunchuckStickY(i) >= 130) {
			transform.Translate(Vector3.forward * (wiimote_getNunchuckStickY(i) - 127) / 300);
		}
	}

	void KeyboardDisplacement () {
		if ( Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) )
			transform.Translate(Vector3.forward * vitesse);
		else if ( Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) )
			transform.Translate(Vector3.back * vitesse);
		if ( Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow) )
			transform.Rotate(Vector3.left * 2);
		else if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
			transform.Rotate(Vector3.right * 2);
		
		if ( Input.GetKeyDown(KeyCode.Space) )
			rigidbody.AddForce(Vector3.up * 350);
	}

	void OnApplicationQuit() {
		wiimote_stop();
	}
}
