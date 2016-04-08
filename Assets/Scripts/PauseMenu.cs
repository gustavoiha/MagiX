using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	enum Menu{None, Pause, Options};
	private Menu currentMenu;

	public static bool isPaused;
	private float windowWidth = 256;
	private float windowHeight = 200;
	public GUISkin newSkin;

	// Use this for initialization
	void Start () {
		isPaused = false;
		currentMenu = Menu.None;
	}
		
	
	void OnGUI(){
		GUI.skin = newSkin;

		if (isPaused && currentMenu == Menu.None) {
			currentMenu = Menu.Pause;
		}
		else if (!isPaused) {
			currentMenu = Menu.None;
		}

		if (currentMenu == Menu.None) {
			Time.timeScale = 1.0f;
			return;
		}

		Time.timeScale = 0.0f;

		switch (currentMenu) {
		case Menu.Options:
			showOptionsMenu ();
			break;
		case Menu.Pause:
			ShowPauseMenu ();
			break;
		}
	}

	void BuildWindow(){
		float windowX = (Screen.width - windowWidth) / 2.0f;
		float windowY = (Screen.height - windowHeight) / 2.0f;
	
		GUILayout.BeginArea (new Rect (windowX, windowY, windowWidth, windowHeight));
	}

	void ShowPauseMenu (){
		
		BuildWindow ();

		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("Resume")) {
			isPaused = false;
			currentMenu = Menu.None;
		}

		if (GUILayout.Button ("Restart"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		GUILayout.EndHorizontal ();

		if (GUILayout.Button ("Options"))
			currentMenu = Menu.Options;

		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("Main Menu"))
			SceneManager.LoadScene ("MainMenu");

		if (GUILayout.Button ("Exit Game"))
			Application.Quit();

		GUILayout.EndHorizontal ();

		GUILayout.EndArea();
	}

	void showOptionsMenu(){
	
		BuildWindow ();

		GUILayout.BeginVertical ("box");

		GUILayout.Label ("Master Volume - (" + AudioListener.volume.ToString("f2") + ")");
		AudioListener.volume = GUILayout.HorizontalSlider (AudioListener.volume, 0.0f, 1.0f);

		int currentQuality = QualitySettings.GetQualityLevel ();
		string qualityName = QualitySettings.names [currentQuality];
		GUILayout.Label("Quality - " + qualityName);

		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("Decrease"))
			QualitySettings.DecreaseLevel ();

		if (GUILayout.Button("Increase"))
			QualitySettings.IncreaseLevel();

		GUILayout.EndHorizontal ();

		if (GUILayout.Button("Back"))
			currentMenu = Menu.Pause;

		GUILayout.EndVertical();
		GUILayout.EndArea();
	
	}
}