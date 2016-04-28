using UnityEngine;
using System.Collections;

public class LightSanctuaryBehaviour : MonoBehaviour {

	//Localização do player
	public Transform player;

	//Distância do círculo
	public float range;

	//dano causado por segundo
	public float damagePerSecond = 5.0f;

	// cura ao player por segundo
	public float healingPerSecond = 2.0f;

	// Duration of skill in seconds
	public float exitTime = 10.0f;

	// Use this for initialization
	void Start () {

		GetComponent<ParticleSystem> ().Play ();

		Destroy (gameObject, exitTime);
	}

	void OnTriggerStay(Collider collider){

		if (collider.gameObject.tag.Equals ("Enemy"))
			collider.gameObject.GetComponent<HealthController> ().TakeDamage ( damagePerSecond  * Time.deltaTime);
		else if (collider.gameObject.tag.Equals ("Player"))
			collider.gameObject.GetComponent<HealthController> ().TakeDamage (-healingPerSecond * Time.deltaTime);

	}
}
