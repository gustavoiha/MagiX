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

	private GameObject particlesOBJ;

	// Use this for initialization
	void Start () {

		gameObject.GetComponentInChildren<ParticleSystem> ().Play ();

		particlesOBJ = gameObject.GetComponentInChildren<ParticleSystem> ().gameObject;

		Destroy (gameObject, exitTime);
	}

	void Update(){
		particlesOBJ.transform.Rotate (0, 0, - 2 * rotationRate * Time.deltaTime);
		gameObject.transform.Rotate (0, rotationRate * Time.deltaTime, 0);
	}

	void OnTriggerStay(Collider collider){

		if (collider.gameObject.CompareTag ("Enemy") || collider.gameObject.CompareTag ("Boss"))
			collider.gameObject.GetComponent<HealthController> ().TakeDamage ( damagePerSecond  * Time.deltaTime);
		else if (collider.gameObject.CompareTag ("Player"))
			collider.gameObject.GetComponent<HealthController> ().TakeDamage (-healingPerSecond * Time.deltaTime);

	}
}
