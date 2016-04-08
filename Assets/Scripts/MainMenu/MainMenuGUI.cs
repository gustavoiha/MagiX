using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuGUI : MonoBehaviour {

	public float windowWidth  = 160;
	public float windowHeight = 100;

	public float distY = 100.0f;

	enum Menu{Main, Settings};
	private Menu currentMenu;

	public GUISkin mGUISkin;

	private Shader backgroundSettingsShader;
	public Shader backgroundMenuShader;
	private Renderer backgroundRenderer;

	void OnStart(){
		currentMenu = Menu.Main;

		backgroundSettingsShader = Shader.Find ("Unlit/backgroundSettingsShader");

		backgroundRenderer = GameObject.Find("Background").GetComponent<Renderer> ();
	}

	void OnGUI(){

		GUI.skin = mGUISkin;

		switch (currentMenu) {
		case Menu.Main:
			showMainMenu ();
			break;
		case Menu.Settings:
			showSettingsMenu ();
			break;
		}
	}

	void buildWindow(){
		float posX = (Screen.width  - windowWidth)  / 2.0f;
		float posY = (Screen.height - windowHeight) / 2.0f + distY;

		GUILayout.BeginArea (new Rect (posX, posY, windowWidth, windowHeight));
	}

	void showMainMenu (){

		buildWindow ();

		if (GUILayout.Button ("Play Game"))
			GameController.startPhase1();

		if (GUILayout.Button ("Settings")) {
			currentMenu = Menu.Settings;
		}

		GUILayout.EndArea ();
	}

	void showSettingsMenu (){
		
		buildWindow ();

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
			currentMenu = Menu.Main;

		GUILayout.EndVertical();
		GUILayout.EndArea();

		backgroundRenderer.material.shader = backgroundSettingsShader;
	}
}
