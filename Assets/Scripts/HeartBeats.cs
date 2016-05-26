using UnityEngine;
using System.Collections;

public class HeartBeats : MonoBehaviour {

	public AudioClip[] heartBeats;

	// Time between a set of heartbeats.
	public float timeBetweenSets = 1.2f;

	public float minTimeBetweenSets = 0.4f;

	private float dynamicTimeBetweenSets;

	// Time between two consecutive beats in a set. (dun-dun) .. (dun-dun) .. (dun-dun). Set = (dun-dun)
	public float timeBetweenBeats = 0.2f;

	private float dynamicTimeBetweenBeats;

	// Health percentage below which heartbeats will start playing.
	public float percentThreshold = 0.6f;

	private float timeFromLastSet;

	private float timeFromLastBeat;

	private AudioSource audioSource;

	private int stepCounter = 0;

	private HealthController healthController;

	private float percent;

	private bool playConsecutiveBeat = false;

	// Use this for initialization
	void Start () {

		audioSource      = gameObject.AddComponent<AudioSource> () as AudioSource;
		healthController = gameObject.GetComponent<HealthController> ();

		dynamicTimeBetweenSets = timeBetweenSets;
		timeFromLastSet		   = timeBetweenSets;

		dynamicTimeBetweenBeats = timeBetweenBeats;
		timeFromLastBeat 	    = timeBetweenBeats;
	}
	
	// Update is called once per frame
	void Update () {

		percent = healthController.health / healthController.maxHealth;

		if (percent > percentThreshold)
			return;

		dynamicTimeBetweenSets = Mathf.Max (timeBetweenSets * percent / percentThreshold, minTimeBetweenSets);

		dynamicTimeBetweenBeats = timeBetweenBeats * dynamicTimeBetweenSets / timeBetweenSets;

		timeFromLastSet -= Time.deltaTime;

		if (playConsecutiveBeat)
			timeFromLastBeat -= Time.deltaTime;

		if (timeFromLastBeat <= 0.0f) {
			PlayNextBeat ();
			playConsecutiveBeat = false;
			timeFromLastBeat = dynamicTimeBetweenBeats;
		}

		if (timeFromLastSet > 0.0f)
			return;

		playConsecutiveBeat = true;

		PlayNextBeat ();

		timeFromLastSet = dynamicTimeBetweenSets;

	}

	public void PlayNextBeat (){

		audioSource.clip = heartBeats[stepCounter];
		audioSource.Play ();

		stepCounter = (stepCounter == 0) ? 1 : 0;
	}
}
