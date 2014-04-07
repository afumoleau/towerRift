using UnityEngine;
using System.Collections;

/// <summary>
///  Class "MenuPlayer" displays the main menu of the player in the first person.
/// </summary>
public class CharacterMenu : MonoBehaviour

{
	
	public GUISkin skin;
	
	private float gldepth = -0.5f;
	private float startTime = 0.1f;
	
	public Material mat;
	
	private long tris = 0;
	private long verts = 0;
	private float savedTimeScale;
	
	private bool showfps;
	private bool showtris;
	private bool showvtx;
	private bool showfpsgraph;

	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;
	
	public int lowFPS = 30;
	public int highFPS = 50;
	
	private bool start = true;

	public Color statColor = Color.yellow;

	public string[] credits= 
	{
		"Programming Project",
		"Programming by A. Fumoleau, A. Girard, R. Verger, V. François",
		"Copyright (c) 2014, University of Bordeaux"} ;

	public Texture[] crediticons;
	
	public enum Page 
	{
		None,Main,Options,Credits
	}
	
	private Page currentPage;
	
	private float[] fpsarray;
	private float fps;
	
	private int toolbarInt = 0;
	private string[]  toolbarstrings =  {"Audio","Graphics","Stats","System"};
	
	/// <summary>
	/// Pause the game and starts the menu at the beginning of the game.
	/// </summary>
	void Start()
	
	{
		fpsarray = new float[Screen.width];
		Time.timeScale = 1;
		PauseGame();
	}

	/// <summary>
	/// Draws the framerate graph.
	/// </summary>
	void OnPostRender() 
	{
		if (showfpsgraph && mat != null) 
		{
			GL.PushMatrix ();
			GL.LoadPixelMatrix();
			for (var i = 0; i < mat.passCount; ++i)
			
			{
				mat.SetPass(i);
				GL.Begin( GL.LINES );
				for (int x=0; x < fpsarray.Length; ++x) 
				{
					GL.Vertex3(x, fpsarray[x], gldepth);
				}
				GL.End();
			}
			GL.PopMatrix();
			ScrollFPS();
		}
	}

	/// <summary>
	/// Computes the framerate graph.
	/// </summary>
	void ScrollFPS() 
	{
		for (int x = 1; x < fpsarray.Length; ++x) 
		{
			fpsarray[x-1]=fpsarray[x];
		}
		if (fps < 1000) 
		{
			fpsarray[fpsarray.Length - 1]=fps;
		}
	}

	/// <summary>
	/// Pause / unpause the game when the player presses the escape.
	/// </summary>
	void LateUpdate () 
	{
		if (showfps || showfpsgraph) 
		{
			FPSUpdate();
		}
		
		if (Input.GetKeyDown("escape")) 
		
		{
			switch (currentPage) 
			
			{
			case Page.None: 
				PauseGame(); 
				Screen.showCursor = true;
				break;
				
			case Page.Main: 
				if (!IsBeginning()) 
					UnPauseGame(); 
				break;

			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}

	/// <summary>
	/// Navigate through the menus.
	/// </summary>
	void OnGUI () 
	{
		if (skin != null) 
		{
			GUI.skin = skin;
		}
		ShowStatNums();
		if (IsGamePaused()) 
		{
			GUI.color = statColor;
			switch (currentPage) 
			{
			case Page.Main: MainPauseMenu(); break;
			case Page.Options: ShowToolbar(); break;
			case Page.Credits: ShowCredits(); break;
			}
		}   
	}

	/// <summary>
	/// Show the toolbar of the menu.
	/// </summary>
	void ShowToolbar() 
	{
		BeginPage(300,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) 
		{
		case 0: VolumeControl(); break;
		case 3: ShowDevice(); break;
		case 1: Qualities(); QualityControl(); break;
		case 2: StatControl(); break;
		}
		EndPage();
	}

	/// <summary>
	/// Show the credits of the game.
	/// </summary>
	void ShowCredits() 
	{
		BeginPage(300,300);
		foreach(string credit in credits) 
		{
			GUILayout.Label(credit);
		}
		foreach( Texture credit in crediticons) 
		{
			GUILayout.Label(credit);
		}
		EndPage();
	}

	/// <summary>
	/// Display a back button in the menu.
	/// </summary>
	void ShowBackButton() 
	{
		if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20),"Back")) 
		{
			currentPage = Page.Main;
		}
	}

	/// <summary>
	/// Display the current configuration of the game.
	/// </summary>
	void ShowDevice() 
	{
		GUILayout.Label("Unity player version "+Application.unityVersion);
		GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
		                SystemInfo.graphicsMemorySize+"MB\n"+
		                SystemInfo.graphicsDeviceVersion+"\n"+
		                SystemInfo.graphicsDeviceVendor);
		GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
		GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
		GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
	}

	/// <summary>
	/// Changes the graphics quality of the game.
	/// </summary>
	void Qualities() 
	{
		switch (QualitySettings.currentLevel) 
		
		{
		case QualityLevel.Fastest:
			GUILayout.Label("Fastest");
			break;
		case QualityLevel.Fast:
			GUILayout.Label("Fast");
			break;
		case QualityLevel.Simple:
			GUILayout.Label("Simple");
			break;
		case QualityLevel.Good:
			GUILayout.Label("Good");
			break;
		case QualityLevel.Beautiful:
			GUILayout.Label("Beautiful");
			break;
		case QualityLevel.Fantastic:
			GUILayout.Label("Fantastic");
			break;
		}
	}

	/// <summary>
	/// Displays two clickable buttons that increase / decrease the graphics quality of the game.
	/// </summary>
	void QualityControl() 
	{
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Decrease")) 
		{
			QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button("Increase")) 
		{
			QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();
	}

	/// <summary>
	/// Displays a slider for controlling the audio volume of the game
	/// </summary>
	void VolumeControl() 
	{
		GUILayout.Label("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
	}

	/// <summary>
	/// 
	/// </summary>
	void StatControl() 
	{
		GUILayout.BeginHorizontal();
		showfps = GUILayout.Toggle(showfps,"FPS");
		showtris = GUILayout.Toggle(showtris,"Triangles");
		showvtx = GUILayout.Toggle(showvtx,"Vertices");
		showfpsgraph = GUILayout.Toggle(showfpsgraph,"FPS Graph");
		GUILayout.EndHorizontal();
	}

	/// <summary>
	/// Computes the framerate.
	/// </summary>
	void FPSUpdate() 
	{
		float delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) 
		{
			fps = 1 / delta;
		}
	}

	/// <summary>
	/// Displays the framerate, the number of triangles, the number of vertices and the framerate graph.
	/// </summary>
	void ShowStatNums() 
	{
		GUILayout.BeginArea(new Rect(Screen.width - 100, 10, 100, 200));
		if (showfps) 
		{
			string fpsstring= fps.ToString ("#,##0 fps");
			GUI.color = Color.Lerp(lowFPSColor, highFPSColor,(fps-lowFPS)/(highFPS-lowFPS));
			GUILayout.Label (fpsstring);
		}
		if (showtris || showvtx) 
		{
			GetObjectStats();
			GUI.color = statColor;
		}
		if (showtris) 
		{
			GUILayout.Label (tris+"tri");
		}
		if (showvtx) 
		{
			GUILayout.Label (verts+"vtx");
		}
		GUILayout.EndArea();
	}

	/// <summary>
	/// Computes the location of the menu page.
	/// </summary>
	/// <param name=width>Width of the page</param>
	/// <param name=height>Height of the page</param>
	void BeginPage(int width, int height) 
	{
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}

	/// <summary>
	/// Displays the back button in the menu if needed.
	/// </summary>
	void EndPage() 
	{
		GUILayout.EndArea();
		if (currentPage != Page.Main) 
		{
			ShowBackButton();
		}
	}

	/// <summary>
	/// Checks if it's the beginning of the game or not.
	/// </summary>
	/// <returns>Return "true" if it's the beginnig of the game, "false" otherwise.</returns>
	bool IsBeginning() 
	{
		return start == true;
	}

	public void SetStart (bool b) 
	{
		start = b;
	}
	
	/// <summary>
	/// Function that displays the buttons on the main menu page.
	/// </summary>
	void MainPauseMenu() 
	{
		BeginPage(200, 200);
		if (GUILayout.Button ((IsBeginning()) ? "Play" : "Continue")) 
		{
			UnPauseGame();
		}
		if (!IsBeginning()) 
		{
			if (GUILayout.Button ("Restart")) 
			{
				RestartGame();
			}
		}
		if (GUILayout.Button ("Options")) 
		{
			currentPage = Page.Options;
		}
		if (GUILayout.Button ("Credits")) 
		{
			currentPage = Page.Credits;
		}
		if (GUILayout.Button ("Quit")) 
		{
			QuitGame();
		}

		EndPage();
	}

	void GetObjectStats() 
	{
		verts = 0;
		tris = 0;
		GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject obj in ob) 
		{
			GetObjectStats(obj);
		}
	}

	/// <summary>
	/// Computes the number of triangles and vertices in the 3D world.
	/// </summary>
	/// <param name=></param>
	void GetObjectStats(GameObject obj) 
	{
		Component[] filters;
		filters = obj.GetComponentsInChildren<MeshFilter>();
		foreach( MeshFilter f  in filters )
		
		{
			tris += f.sharedMesh.triangles.Length/3;
			verts += f.sharedMesh.vertexCount;
		}
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	void PauseGame() 
	{
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		Screen.showCursor = true;
		AudioListener.pause = true;
		currentPage = Page.Main;
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	void UnPauseGame() 
	{
		Time.timeScale = savedTimeScale;
		Screen.showCursor = false;
		AudioListener.pause = false;
		currentPage = Page.None;
		
		if (IsBeginning()) 
		{
			start = false;
		}
	}

	/// <summary>
	/// Restart the game.
	/// </summary>
	public void RestartGame() 
	{
		Application.LoadLevel("Game");
		Time.timeScale = 1;
	}

	/// <summary>
	/// Quit the game.
	/// </summary>
	void QuitGame() 
	{
		Application.Quit();
	}

	/// <summary>
	/// Returns whether the game is paused.
	/// </summary>
	bool IsGamePaused() 
	{
		return (Time.timeScale == 0);
	}

	/// <summary>
	/// Pauses the sound if the game is paused.
	/// </summary>
	void OnApplicationPause(bool pause) 
	{
		if (IsGamePaused()) 
		{
			AudioListener.pause = true;
		}
	}
}