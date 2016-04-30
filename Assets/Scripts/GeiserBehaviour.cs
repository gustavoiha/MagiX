using UnityEngine;
using System.Collections;

public class GeiserBehaviour : MonoBehaviour {

	public float intervalTime = 5.0f;

	public float intervalTimeRandomIncrement = 2.0f;

	public float activeTime = 2.0f;

	public float activeTimeRandomIncrement = 1.0f;

	public float damageRate = 3.0f;

	private bool activeState = false;

	private float timeLeft = 0.0f;

	private ParticleSystem geiserParticleSystem;

	// Use this for initialization
	void Start () {
	
		geiserParticleSystem = GetComponentInChildren<ParticleSystem> ();

		// Starts inactive
		timeLeft = intervalTime;

		var emission = geiserParticleSystem.emission;
		emission.enabled = activeState;
	}
	
	// Update is called once per frame
	void Update () {

		if (timeLeft <= 0.0f) {
			
			if (!activeState) {
				
				activeState = true;

				timeLeft = activeTime + activeTimeRandomIncrement * Random.value;

			}
			else {
				
				activeState = false;

				timeLeft = intervalTime + intervalTimeRandomIncrement * Random.value;

			}

			var emission = geiserParticleSystem.emission;
			emission.enabled = activeState;

		}

		timeLeft -= Time.deltaTime;
	}

	void OnTriggerStay(Collider collider){

		if (!collider.CompareTag ("Player") || !activeState)
			return;

		collider.gameObject.GetComponent<HealthController> ().TakeDamage (damageRate * Time.deltaTime);

	}
}
