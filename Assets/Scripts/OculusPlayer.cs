using UnityEngine;
using System.Collections;

/// <summary>
///  Class "OculusPlayer"
/// </summary>
public class OculusPlayer : MonoBehaviour
{
	public Transform cursorCamera;
	public Transform rightHand;
	public Transform head;
	public Transform leftHand;

	public Camera leftCamera;
	public Camera rightCamera;
	public Collider ground;

	private TowerManager towerManager;
	private HintManager hintManager;
	private Transform manipulatedObject;

	void Start ()
	{
		hintManager = HintManager.Instance;
		towerManager = TowerManager.Instance;

		switch(PlayerPrefs.GetInt("gameMode"))
		{
			case 0 : // Only standard player
				gameObject.SetActive(false);
				break;
			case 1 : // Only Rift player
				leftCamera.rect = new Rect(0f, 0f, 0.5f, 1f);
				rightCamera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
				break;
			default : // Both players
				leftCamera.rect = new Rect(0.5f, 0f, 0.25f, 1f);
				rightCamera.rect = new Rect(0.75f, 0f, 0.25f, 1f);
				break;
		}
		manipulatedObject = null;
	}

	void Update ()
	{
		Vector3 rayOrigin = cursorCamera.position;
		Vector3 rayDirection = Vector3.Normalize(rightHand.position - rayOrigin);

		Ray ray = new Ray(rayOrigin, rayDirection);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if(hit.collider.gameObject.CompareTag("Grid"))
			{
				if(head.position.x < leftHand.position.x)
					hintManager.pin(hit.point);
			}
			if(hit.collider.gameObject.CompareTag("towerCube"))
			{
				if(manipulatedObject == null)
					manipulatedObject = hit.collider.transform;
			}
		}
		
		if(manipulatedObject != null)
		{
			if(manipulatedObject.parent != towerManager.transform)
				manipulatedObject = null;
			else
			{
				if(ground.Raycast(ray, out hit, 1000))
					manipulatedObject.position = hit.point + new Vector3(0f, 2.5f, 0f);
			}
		}
	}
}
