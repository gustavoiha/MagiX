using UnityEngine;
using System.Collections;

public class SoundAlternate : MonoBehaviour {

	public AudioClip[] sounds;

	private AudioSource audioSource;

	private int soundCounter = 0;

	private int currentStepSound = 0;

	public bool is3D;

	public float dopplerLevel = 0.0f;

	public float minDistance = 20.0f;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource> () as AudioSource;
		audioSource.spatialBlend = (is3D) ? 1.0f : 0.0f;
		audioSource.dopplerLevel = this.dopplerLevel;
		audioSource.minDistance  = this.minDistance;
	}
	
	public void PlayNextSound (){

		audioSource.clip = sounds[currentStepSound + soundCounter];
		audioSource.Play ();

		soundCounter = (soundCounter == 0) ? 1 : 0;
	}
}
