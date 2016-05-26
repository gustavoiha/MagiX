using UnityEngine;
using System.Collections;

public class DeathPitBehaviour : MonoBehaviour {

	public string tagsToAvoid;

	void OnTriggerEnter (Collider collider) {

		if (tagsToAvoid.Contains (collider.tag))
			return;

		HealthController healthController = collider.gameObject.GetComponent<HealthController> ();

		if (healthController != null)
			healthController.TakeDamage (healthController.health);
	}
}
