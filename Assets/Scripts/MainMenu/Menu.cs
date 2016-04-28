using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void NewGameButton ()
	{
		GameController.startPhase("RebelVillage");
	}
	public void ExitGame ()
	{
		Application.Quit();
	}
}
