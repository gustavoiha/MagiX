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
	public string tagsToReceiveDamage = "";

	/// <summary>
	/// Checks if collided with an enemy
	/// </summary>
	public bool collisionEnemy = false;

	/// <summary>
	/// Detects collision and do damage
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter(Collision collision) {

		// Checks if tagsToReceiveDamage has tag of the object that collided
		if(tagsToReceiveDamage.Contains(collision.collider.gameObject.tag))
		{
			this.collisionEnemy = true;
			HealthController enemy = collision.collider.GetComponent<HealthController>() as HealthController;
			enemy.TakeDamage(damageOnHit);
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Sets the damage on hit.
	/// </summary>
	/// <param name="newDamage">New damage.</param>
	public void setDamageOnHit(float newDamage){
		this.damageOnHit = newDamage;
	}
}
