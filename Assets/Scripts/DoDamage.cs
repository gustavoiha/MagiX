using UnityEngine;
using System.Collections;

public class DoDamage : MonoBehaviour {

	/// <summary>
	/// The damage on hit.
	/// </summary>
	public float damageOnHit;

	public new bool enabled;

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
	/// Checks if collided with an enemy
	/// </summary>
	//private bool collisionEnemy = false;

	void Start () {
		enabled = true;
	}

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
			if (collider.gameObject.CompareTag ("Player") && !GameController.usingShield) {
				enemy.TakeDamage (damageOnHit);
				enabled = !disableAfterHit;
			}

			if (collider.gameObject.CompareTag ("Enemy")) {
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

	/// <summary>
	/// Checks if stopped colliding enemy
	/// </summary>
	/// <param name="collider">Collider.</param>
	/*void OnTriggerExit (Collider collider){
		// Checks if tagsToReceiveDamage has tag of the object that collided
		if (tagsToReceiveDamage.Contains(collider.gameObject.tag) && collider.gameObject.tag != "")
		{
			this.collisionEnemy = false;
		}
	}*/

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
