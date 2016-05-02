using UnityEngine;
using System.Collections;

public class CollectibleBehaviour : MonoBehaviour {

	public float manaGain = 1.0f;

	void OnTriggerEnter(Collider collider){

		if (!collider.gameObject.CompareTag ("Player"))
			return;

		collider.gameObject.GetComponent<HealthController> ().DecreaseMana (- manaGain);
		Destroy (gameObject);
	}
}
