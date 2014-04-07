using UnityEngine;
using System.Collections;

/// <summary>
///  Manages the camera of the character
/// </summary>
public class CharacterCamera : MonoBehaviour
{
	private Character character;
	public Crystal crystal;

	public float sensitivity=5f;
	private float xRotation;
	private float yRotation;
	private float currentXRotation;
	private float currentYRotation;
	private float xRotationV;
	private float yRotationV;
	public float lookSmoothDamp=.1f;
	public Texture2D barHealth;
	public Texture2D supBarHealth;

	private float timeSinceLatestFPSCount = 0f;
	private int framesCounted = 0;
	private int frames = 0;

	/// <summary>
	/// Initializes the character's camera
	/// </summary>
	void Start()
	{
		character = Character.Instance;
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

		timeSinceLatestFPSCount = Time.realtimeSinceStartup;
	}

	/// <summary>
	/// Called each frame to make the camera follow the mouse cursor
	/// </summary>
	void FixedUpdate() 
	{
		xRotation -= Input.GetAxis ("Mouse Y") * sensitivity;
		yRotation += Input.GetAxis ("Mouse X") * sensitivity;

		xRotation = Mathf.Clamp (xRotation, -90, 90);

		currentXRotation = Mathf.SmoothDamp (currentXRotation, xRotation,ref xRotationV, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp (currentYRotation, yRotation,ref yRotationV, lookSmoothDamp);

		transform.rotation = Quaternion.Euler (currentXRotation, currentYRotation, 0);
		character.transform.rotation = Quaternion.Euler(0.0f, currentYRotation, 0.0f);
	}

	/// <summary>
	/// Called each frame to count the framerate
	/// </summary>
	void Update()
	{
		++frames;
		if(Time.realtimeSinceStartup - timeSinceLatestFPSCount >= 1f)
		{
			timeSinceLatestFPSCount = Time.realtimeSinceStartup;
			framesCounted = frames;
			frames = 0;
		}
	}

	/// <summary>
	/// Displays character's Head-Up Display
	/// </summary>
	void OnGUI()
	{
		GUI.Label(new Rect (25, 25, 200, 20), "Framerate\t\t\t: "+framesCounted+" FPS");

		//GUI.Label(new Rect (25, 45, 200, 20), "Crystal Health\t: "+crystal.health);
		GUI.DrawTexture(new Rect(25, 45, barHealth.width * crystal.health / crystal.maxHealth, barHealth.height), barHealth);
		GUI.DrawTexture(new Rect(25, 45, supBarHealth.width, barHealth.height), supBarHealth);

		GUI.Label(new Rect (25, 65, 200, 20), "Money\t\t\t\t: "+character.money);
	}
}
