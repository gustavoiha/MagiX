using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void NewGameButton ()
	{
		SceneManager.LoadScene ("RebelVillage");
	}
	public void ExitGame ()
	{
		Application.Quit();
	}
}
