using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static int totalItensToGet = 5;

	public static int itensLeft;

	// Use this for initialization
	void Start () {
		itensLeft = totalItensToGet;
		GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<ProgressBarController> ().SetBossMenuState (false);
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

	public static void aquiredItem(){
		
		itensLeft--;
		
		Debug.Log ("itens left: " + itensLeft);

		if (itensLeft == 0) {
			GameObject.FindGameObjectWithTag ("BossShield").GetComponent<BossShieldScript> ().startBoss ();
		}

	}

}