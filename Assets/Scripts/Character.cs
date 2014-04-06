using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Character : Singleton<Character>
{
	protected Character(){}

	public float speed = 10f;
	public float jumpForce = 350f;
	public GameObject menuInGame;
	public float money = 100;
	public float towerBaseCost = 100;
	public AudioClip shootSound;
	public Transform shootEffect;

	private TowerManager towerManager;
	private Camera camera;
	private CharacterController controller;
	private Vector3 startPos;
	private Vector3 endPos;

	void Start()
	{
		towerManager = TowerManager.Instance;
		controller = GetComponent<CharacterController>();
		camera = transform.Find("Camera").GetComponent<Camera>();
	}
	
	void Update()
	{
		KeyboardMove();

		if(Input.GetKeyDown(KeyCode.A) && money >= towerBaseCost)
		{
			spawnTowerBase();
			money -= towerBaseCost;
		}

		if (Input.GetMouseButtonDown(0))
			interact();
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

	void interact()
	{
		Ray cameraRay = camera.ScreenPointToRay(new Vector3((camera.rect.x + camera.rect.width / 2f) * Screen.width, (camera.rect.y + camera.rect.height / 2f) * Screen.height, 0f));
		RaycastHit hit;

		if (Time.timeScale == 1)
			audio.PlayOneShot(shootSound);

		if(Physics.Raycast(cameraRay, out hit, 1000))
		{
			if(hit.transform.CompareTag("Enemy"))
			{
				Transform particleClone = Instantiate(shootEffect,hit.point,Quaternion.LookRotation(hit.normal)) as Transform;
				StartCoroutine("shoot",particleClone);
				//Send the damage at the method ApplyDamage within enemy Script
				hit.transform.GetComponent<Enemy>().hit();
			}
			if(hit.transform.CompareTag("towerCube"))
			{
				//StartPos, endPos to lerp cubeTower near to the player
				startPos = hit.transform.position;
				endPos = transform.position + new Vector3(0.0f,2.0f,2.0f);

				//Call coroutine function to move our hit transform from startLerp to endLerp
				StartCoroutine("manipulateObject", hit.transform);
			}
		}
	}

	IEnumerator manipulateObject(Transform thisTransform) 
	{
		double i= 0.0;
		double rate = 0.5;
		while (i < 1.0) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, ((float) i)* speed); 
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator shoot(Transform thisTransform) 
	{
		yield return new WaitForSeconds(1.0f);
		GameObject.Destroy(thisTransform.gameObject);
	}
}