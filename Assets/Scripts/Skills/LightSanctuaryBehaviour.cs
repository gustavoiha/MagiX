using UnityEngine;
using System.Collections;

public class LightSanctuaryBehaviour : MonoBehaviour {

	//dano causado por segundo
	public float damagePerSecond = 5.0f;

	// cura ao player por segundo
	public float healingPerSecond = 2.0f;

	// Duration of skill in seconds
	public float exitTime = 10.0f;

	// Rotation in degrees per second
	public float rotationRate = 10.0f;

	// Use this for initialization
	void Start () {

		GetComponent<ParticleSystem> ().Play ();

		Destroy (gameObject.transform.root.gameObject, exitTime);
	}

	void Update(){
		gameObject.transform.root.gameObject.transform.Rotate (0, rotationRate * Time.deltaTime, 0);
		gameObject.transform.Rotate (0, 0, - 2 *rotationRate * Time.deltaTime);
	}

	void OnTriggerStay(Collider collider){

		if (collider.gameObject.tag.Equals ("Enemy") || collider.gameObject.tag.Equals ("Boss"))
			collider.gameObject.GetComponent<HealthController> ().TakeDamage ( damagePerSecond  * Time.deltaTime);
		else if (collider.gameObject.tag.Equals ("Player"))
			collider.gameObject.GetComponent<HealthController> ().TakeDamage (-healingPerSecond * Time.deltaTime);

	}
}
