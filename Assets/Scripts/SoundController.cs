using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	new AudioSource audio;
    // AudioClips que serão usados
    public AudioClip environment;
	public AudioClip playerStep;
    public AudioClip playerBasicAtt;
    public AudioClip playerLightArrow;
    public AudioClip playerLightBall;
    public AudioClip playerLightSanctuary;
    public AudioClip playerDeffenseDome;

    //id dos sons emitidos pelo player
    public const int WALK_PLAYER        = 0;
    public const int BASIC_ATTACK       = 1;
    public const int LIGHT_ARROW        = 2;
    public const int LIGHT_BALL         = 3;
    public const int LIGHT_SANCTUARY    = 4;
    public const int DEFENCE_DOME       = 5;

    //som ambiente
    public void Start() {
		audio = Camera.main.GetComponent<AudioSource> ();
		audio.clip = environment;
		audio.Play();
	}

	// sons do player
	public void PlayerSounds (int sound) {
		audio = GameObject.FindGameObjectWithTag ("Player").GetComponent<AudioSource> ();
		
     
	}
}
