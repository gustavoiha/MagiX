using UnityEngine;
using System.Collections;

public class DeathPitBehaviour : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		HealthController healthController = collider.gameObject.GetComponent<HealthController> ();

		if (healthController != null)
			healthController.TakeDamage (healthController.health);
	}
}
