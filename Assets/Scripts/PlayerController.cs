using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	/*Move Variable*/
	public float speedRunVar=7.0f;
	public float speedMovement = 5.0f;
	public Vector3 MoveDirection= new Vector3();
	public bool isRunning = false;

	/*Jump Collision variable*/
	private ContactPoint contact;
	public bool jumpClimax;
	
	/*Jump Variable*/
	public float jumpSpeed =500.0f;
	public Vector3 jumpDirection = new Vector3();
	public bool isGrounded = true;
	public bool jumping = false;
	public bool inAir = false;
	public bool again = false;
	public float airControl =0.5f;
	private float horizontal;
	private float vertical;
	
	void onCollisionStay(Collision collisionInfo){
		contact = collisionInfo.contacts[0];
		if (inAir || jumping) {
			rigidbody.AddForceAtPosition (-rigidbody.velocity, contact.point);
		}
	}
	
	void Movement(){
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded  ) {
			jumping=true;
			inAir = true;
			isGrounded = false;
			jumpDirection = MoveDirection;
			rigidbody.AddForce((transform.up/2f)* jumpSpeed);
			
		}
		/*
		if (isGrounded) 
			this.transform.Translate ((MoveDirection.normalized * speedMovement) * Time.deltaTime);
		*/
		else if (jumping || inAir) {
			this.transform.Translate ((jumpDirection * speedMovement * airControl) * Time.deltaTime);
		}
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			isRunning = true;
			speedMovement = speedRunVar;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
				isRunning = false;
				speedMovementInitialise (5.0f);
		}
		else {
				horizontal = Input.GetAxisRaw ("Horizontal");
				vertical = Input.GetAxisRaw ("Vertical");
				MoveDirection.Set (horizontal, 0.0f, vertical);
				transform.Translate (MoveDirection.normalized * speedMovement * Time.deltaTime);
		}
	}
	void speedMovementInitialise(float a){
		speedMovement = a;
	}
	
	void FixedUpdate()
	{
		
		/*if (!isGrounded) {*/
		if (Physics.Raycast (transform.position, -transform.up, 0.5f )) {
			isGrounded = true;
			jumping = false;
			inAir = false;
			jumpClimax = true;	
		} 
		else if (!inAir) {
			inAir = true;
			jumpDirection = MoveDirection;
		}
		else if(inAir && rigidbody.velocity.y == 0.0f){
			jumpClimax = true;
		}                       
		//}
		Movement ();
	}
	
	void onCollisionExit(Collision collisionAdvert){
		isGrounded = false;
	}
}
