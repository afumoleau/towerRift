using UnityEngine;
using System.Collections;

public class OculusPlayer : MonoBehaviour
{
	public float speed = 10;
	public Transform cursor;
	public Transform mapCursor;
	public PinManager pinManager;

	void Start ()
	{	
	}

	void Update ()
	{
		// Move
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 moveVector = Vector3.zero;
		if (Input.GetKey(KeyCode.LeftArrow))
			moveVector += Vector3.Cross(transform.forward, transform.up);
		if (Input.GetKey(KeyCode.RightArrow))
			moveVector += -Vector3.Cross(transform.forward, transform.up);
		if (Input.GetKey(KeyCode.UpArrow))
			moveVector += transform.forward;
		if (Input.GetKey(KeyCode.DownArrow))
			moveVector += -transform.forward;
		controller.Move(moveVector.normalized * speed * Time.deltaTime);
		
		cursor.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
		/*
		Ray ray = new Ray(Camera.main.transform.position, (cursor.position - Camera.main.transform.position).normalized);
		Debug.Log(ray);
		Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f);
		*/

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Grid")
        {
            mapCursor.position = hit.point;
            mapCursor.gameObject.SetActive(true);
			
			if(Input.GetMouseButtonDown(1))
				pinManager.pin(mapCursor.position);
        }
        else
            mapCursor.gameObject.SetActive(false);

/*		// Cast ray
		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log(Camera.main.transform.position);
			RaycastHit hit;
	        if (Physics.Raycast(ray, out hit))
	        	Debug.Log("HIT");
		}*/
	}
}
