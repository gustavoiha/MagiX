using UnityEngine;
using System.Collections;

public class LifeHealerBehaviour : MonoBehaviour {

	/// <summary>
	/// Life healed per second
	/// </summary>
	public float lifeHealingRate = 1.0f;

	void OnTriggerStay(Collider collider){
		
		if (!collider.gameObject.tag.Equals ("Player"))
			return;

		collider.gameObject.GetComponent<HealthController> ().TakeDamage (- lifeHealingRate * Time.deltaTime);
		//Debug.Log("Healing. Life: " + collider.gameObject.GetComponent<HealthController> ().health);
	}
}
