using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip[] audioClips;

	private AudioSource[] audioSources;

	public bool is3D;

	public float minDistance = 20.0f;

	// Use this for initialization
	void Start () {
		audioSources = new AudioSource[audioClips.Length];

		for (int a = 0; a < audioSources.Length; a++) {
			audioSources [a] = gameObject.AddComponent<AudioSource> () as AudioSource;
			audioSources [a].spatialBlend = (is3D) ? 1.0f : 0.0f;
			audioSources [a].minDistance = this.minDistance;
		}
	}

	public void PlaySound (int index){
		audioSources [index].clip = audioClips [index];
		audioSources [index].Play ();
	}
}
