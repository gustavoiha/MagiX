using UnityEngine;
using System.Collections;

public class DoDamage : MonoBehaviour {

	/// <summary>
	/// The damage on hit.
	/// </summary>
	public float damageOnHit;

	/// <summary>
	/// Damage per second
	/// </summary>
	public float damageContinuous = 0.0f;

	public new bool enabled = true;

    public bool toBeDestroyedOnHit;

	public bool disableAfterHit = false;

	public bool explosionOnHit = false;

	public float explosionKillTime = 5.0f;

	public GameObject explosion;

	/// <summary>
	/// String that contains every gameobject tag you wish to take damage.
	/// An example: "enemy1, enemy2, boss"
	/// In this case, this script will try to acess a method called TakeDamage(float damage) in
	/// an script called HealthController in the enemy.
	/// </summary>
	public string tagsToReceiveDamage = "Enemy";

	/// <summary>
	/// Detects collision and do damage
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider collider) {

		if (!enabled)
			return;

		// Checks if tagsToReceiveDamage has tag of the object that collided
		if(tagsToReceiveDamage.Contains(collider.gameObject.tag) && collider.gameObject.tag != "") {
			//this.collisionEnemy = true;
			HealthController enemy = collider.GetComponent<HealthController> () as HealthController;
			/*if (collider.gameObject.CompareTag ("Player") && !GameController.usingShield) {
				enemy.TakeDamage (damageOnHit);
				enabled = !disableAfterHit;
			}

			if (collider.gameObject.CompareTag ("Enemy") || collider.gameObject.CompareTag ("Boss")) {
				enemy.TakeDamage (damageOnHit);
				enabled = !disableAfterHit;
			}*/

			if (tagsToReceiveDamage.Contains (collider.tag)) {
				enemy.TakeDamage (damageOnHit);
				enabled = !disableAfterHit;
			}

			if (toBeDestroyedOnHit)
                Destroy (gameObject);

			if (explosionOnHit && explosion != null){
				GameObject newExplosion = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
				Destroy (newExplosion, explosionKillTime);
			}
		}
	}

	void OnTriggerStay (Collider collider){
		if (!enabled)
			return;

		// Checks if tagsToReceiveDamage has tag of the object that collided
		if(tagsToReceiveDamage.Contains(collider.gameObject.tag) && collider.gameObject.tag != "") {
			
			HealthController enemy = collider.GetComponent<HealthController> () as HealthController;

			if (tagsToReceiveDamage.Contains (collider.tag)) {
				enemy.TakeDamage (damageContinuous * Time.deltaTime);
			}
		}
	}
		
	/// <summary>
	/// Sets the damage on hit.
	/// </summary>
	/// <param name="newDamage">New damage.</param>
	public void setDamageOnHit (float newDamage){
		this.damageOnHit = newDamage;
	}

	//public bool collidedEnemy (){
	//	return collisionEnemy;
	//}
}
