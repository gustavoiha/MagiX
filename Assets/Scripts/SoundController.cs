using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	new AudioSource audio;
	public AudioClip environment;
	public AudioClip playerStep;


	//som ambiente
	public void Start() {
		audio = Camera.main.GetComponent<AudioSource> ();
		audio.clip = environment;
		audio.Play();
	}

	// player andando
	public void WalkingPlayer () {
		audio = GameObject.FindGameObjectWithTag ("Player").GetComponent<AudioSource> ();
		audio.clip = playerStep;
		audio.Play ();
	}
}
