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
    public AudioClip playerDead;
    public AudioClip win;
    public AudioClip lose;
	public AudioClip bossBasicAttack;
	public AudioClip bossImplosion;
	public AudioClip bossExplosion;
	public AudioClip bossStorm;

    //id dos sons emitidos pelo player
    public const int WALK_PLAYER        = 0;
    public const int BASIC_ATTACK       = 1;
    public const int LIGHT_ARROW        = 2;
    public const int LIGHT_BALL         = 3;
    public const int LIGHT_SANCTUARY    = 4;
    public const int DEFENCE_DOME       = 5;
    public const int DEAD_PLAYER        = 6;
	public const int BOSS_BASIC_ATTACK  = 7;
	public const int IMPLOSION  = 8;
	public const int EXPLOSION  = 9;
	public const int STORM      = 10;

    // vitória e derrota
    public const int WIN    = 0;
    public const int LOSE = 1;

    //som ambiente
    public void Start() {
		audio = Camera.main.GetComponent<AudioSource> ();
		audio.clip = environment;
		audio.Play();
	}

	// sons do player
	public void PlayerSounds (int sound, bool playing) {
		audio = GameObject.FindGameObjectWithTag ("Player").GetComponent<AudioSource> ();
		switch(sound)
        {
            case WALK_PLAYER:
			PlayerSoundsController(playerStep, playing, false, "Player");
                break;

            case BASIC_ATTACK:
			PlayerSoundsController(playerBasicAtt, playing, true, "Player");
                break;

            case LIGHT_ARROW:
			PlayerSoundsController(playerLightArrow, playing, true, "Player");
                break;

            case LIGHT_BALL:
			PlayerSoundsController(playerLightBall, playing, true, "Player");
                break;

            case LIGHT_SANCTUARY:
			PlayerSoundsController(playerBasicAtt, playing, true, "Player");
                break;

            case DEFENCE_DOME:
			PlayerSoundsController(playerDeffenseDome, playing, true, "Player");
                break;

            case DEAD_PLAYER:
                PlayerSoundsController(playerDead, playing, false, "Player");
                break;
            

        }
     
	}
		
	private void PlayerSoundsController(AudioClip a, bool playing, bool isSkill, string tag)
    {
        audio = GameObject.FindGameObjectWithTag(tag).GetComponent<AudioSource>();
        if (playing)
        {
            if (!isSkill)
            {
                if (!audio.isPlaying)
                {
                    audio.clip = a;
                    audio.Play();
                }
                else audio.Stop();
            }
            else
            {
                audio.clip = a;
                audio.loop = false;
                audio.Play();
            }
        }
    }

    // vitória e derrota
    public void EndSounds(int sound)
    {
        audio = Camera.main.GetComponent<AudioSource>();

        switch (sound)
        {
            case WIN:
                audio.clip = win;
                audio.loop = false;
                audio.Play();
                break;
            case LOSE:
                audio.clip = lose;
                audio.loop = false;
                audio.Play();
                break;
        }
        
    }

	public void BossSounds (int sound){
		switch (sound) {
		case BOSS_BASIC_ATTACK:
			PlayerSoundsController (bossBasicAttack, true, true, "Boss");
			break;
		case IMPLOSION:
			PlayerSoundsController (bossImplosion, true, true, "Boss");
			break;
		case EXPLOSION:
			PlayerSoundsController (bossExplosion, true, true, "Boss");
			break;
		case STORM:
			PlayerSoundsController (bossStorm, true, true, "Boss");
			break;
		}
	}
}
