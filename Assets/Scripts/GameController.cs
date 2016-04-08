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


	/**
	 * Use this method to start the desired phase (@int phaseNumber).
	 */
	public static void startPhase(int phaseNumber){
		switch (phaseNumber) {
		case 1:
			startPhase1 ();
			break;
		case 2:
			startPhase2 ();
			break;
		case 3:
			startPhase3 ();
			break;
		}
	}

	/**
	 * Change the following methods (startPhase1, startPhase2 and startPhase3)
	 * to do whatever you want. They are called to begin a phase, so you can make
	 * an animation appear before the actual phase
	 */

	public static void startPhase1() {

		// Do something here, like play a video

		// Finally, start the game
		SceneManager.LoadScene ("Phase1");
	}

	public static void startPhase2() {

		// Do something here, like play a video

		// Finally, start the game
		SceneManager.LoadScene ("Phase2");
	}

	public static void startPhase3() {

		// Do something here, like play a video

		// Finally, start the game
		SceneManager.LoadScene ("Phase3");
	}
}