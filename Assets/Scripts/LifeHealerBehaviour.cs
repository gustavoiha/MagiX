using UnityEngine;
using System.Collections;

public class LifeHealerBehaviour : MonoBehaviour {

	/// <summary>
	/// Life healed per second
	/// </summary>
	public float lifeHealingRate = 1.0f;

	/// <summary>
	/// Mana gained per second
	/// </summary>
	public float ManaGainRate = 1.0f;

	void OnTriggerStay(Collider collider){
		
		if (!collider.gameObject.tag.Equals ("Player"))
			return;

		HealthController healthController = collider.gameObject.GetComponent<HealthController> ();

		healthController.TakeDamage   (- lifeHealingRate * Time.deltaTime);
		healthController.DecreaseMana (- ManaGainRate    * Time.deltaTime);
	}
}
