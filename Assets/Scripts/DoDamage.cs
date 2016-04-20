using UnityEngine;
using System.Collections;

public class DoDamage : MonoBehaviour {

	/// <summary>
	/// The damage on hit.
	/// </summary>
	public float damageOnHit;

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
	private bool collisionEnemy = false;

	/// <summary>
	/// Detects collision and do damage
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider collider) {

		// Checks if tagsToReceiveDamage has tag of the object that collided
		if(tagsToReceiveDamage.Contains(collider.gameObject.tag) && collider.gameObject.tag != "")
		{
			this.collisionEnemy = true;
			HealthController enemy = collider.GetComponent<HealthController>() as HealthController;
			enemy.TakeDamage(damageOnHit);
            Destroy(gameObject);
		}
	}

	/// <summary>
	/// Checks if stopped colliding enemy
	/// </summary>
	/// <param name="collider">Collider.</param>
	void OnTriggerExit(Collider collider){
		// Checks if tagsToReceiveDamage has tag of the object that collided
		if(tagsToReceiveDamage.Contains(collider.gameObject.tag) && collider.gameObject.tag != "")
		{
			this.collisionEnemy = false;
		}
	}

	/// <summary>
	/// Sets the damage on hit.
	/// </summary>
	/// <param name="newDamage">New damage.</param>
	public void setDamageOnHit(float newDamage){
		this.damageOnHit = newDamage;
	}

	public bool collidedEnemy(){
		return collisionEnemy;
	}
}
