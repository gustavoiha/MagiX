using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	/********************************************************
	 * VERY IMPORTANT:
	 * DO NOT EVER ADD THIS SCRIPT TO A GAMEOBJECT IN A SCENE
	 * INSTEAD, CREATE STATIC VARIABLES TO DO WHAT YOU WANT
	 ********************************************************/



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/// <summary>
	/// Starts the desired phase.
	/// </summary>
	/// <param name="phaseName">Phase name.</param>
	public static void startPhase(string phaseName){

		// Checks if the scene exists and can be loaded
		if (Application.CanStreamedLevelBeLoaded(phaseName))
			SceneManager.LoadScene (phaseName);
	}


}