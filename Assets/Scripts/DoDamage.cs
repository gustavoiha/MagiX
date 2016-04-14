using UnityEngine;
using System.Collections;

public class DoDamage : MonoBehaviour {

	/// <summary>
	/// The damage on hit.
	/// </summary>
	public float damageOnHit;

	/// <summary>
	/// Detects collision and do damage
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider collider)
	{
		//if(collider.gameObject.tag == "Enemy")
		{
			//Mob enemy = other.GetComponent<Mob>();
			//enemy.TakeDamage(damage);
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
