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

	public float minTimeBetweenBeats = 0.1f;

	private float dynamicTimeBetweenBeats;

	// Health percentage below which heartbeats will start playing.
	public float percentThreshold = 0.6f;

	private float timeFromLastSet;

	private float timeFromLastBeat;

	private AudioSource[] audioSource;

	private int stepCounter = 0;

	private HealthController healthController;

	private float percent;

	private bool playConsecutiveBeat = false;

	// Use this for initialization
	void Start () {

		audioSource = new AudioSource[heartBeats.Length];

		for (int a = 0; a < heartBeats.Length; a++) {
			audioSource [a]  = gameObject.AddComponent<AudioSource> () as AudioSource;
			audioSource [a].clip = heartBeats [a];
		}

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

		dynamicTimeBetweenSets  = Mathf.Max (timeBetweenSets * percent / percentThreshold, minTimeBetweenSets);

		dynamicTimeBetweenBeats = Mathf.Max (timeBetweenBeats * dynamicTimeBetweenSets / timeBetweenSets, minTimeBetweenBeats);

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

		Debug.Log ("playing");

		audioSource [stepCounter].Play ();

		stepCounter = (stepCounter == 0) ? 1 : 0;
	}
}
