using UnityEngine;
using System.Collections;

public class showFps : MonoBehaviour {

	public GUIText gui;
	private float updateInterval = 1.0f;
	private float lastInterval; // Last interval end time
	private float frames = 0.0f; // Frames over current interval
	private bool fpsOn;

void Start()
{
	fpsOn = false;
	gui.transform.position = Camera.main.ScreenToViewportPoint (new Vector3(15, 15, 0));
	gui.text = "";
	lastInterval = Time.realtimeSinceStartup;
	frames = 0;
}



void OnDisable ()
{
	if (gui)
		DestroyImmediate (gui.gameObject);
}

void Update()
{
		if (Input.GetKeyDown (KeyCode.F) || fpsOn) {
			fpsOn = true;
			++frames;
			var timeNow = Time.realtimeSinceStartup;
			if (timeNow > lastInterval + updateInterval) {
								if (!gui) {
										GameObject go = new GameObject ("FPS Display");
										go.hideFlags = HideFlags.HideAndDontSave;
										go.transform.position = new Vector3 (0, 0, 0);
										gui = go.guiText;
										gui.pixelOffset = new Vector2 (5, 20);
								}
								float fps = frames / (timeNow - lastInterval);
								float ms = 1000.0f / Mathf.Max (fps, 0.00001f);
								gui.text = ms.ToString ("f1") + "ms " + fps.ToString ("f2") + "FPS";
								frames = 0;
								lastInterval = timeNow;
						}
				}
}
}