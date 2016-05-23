using UnityEngine;
using System.Collections;

public class FootStepsController : MonoBehaviour {

	public AudioClip[] footSteps;

	private AudioSource audioSource;

	private int stepCounter = 0;

	//private bool isMoving = false;

	//0, 2, 4 ... bacause each step sound has two audio clips (left and right feet)
	public int currentStepSound = 0;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource> () as AudioSource;
	}

	public  void PlayNextStep (){
		
		audioSource.clip = footSteps[currentStepSound + stepCounter];
		audioSource.Play ();

		stepCounter = (stepCounter == 0) ? 1 : 0;

		//Debug.Log ("Footstep!! Sound: " + stepCounter.ToString ());
	}
}
