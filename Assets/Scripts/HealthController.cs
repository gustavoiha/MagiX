using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	/// <summary>
	/// Starting health
	/// </summary>
	public float maxHealth = 20.0f;

	/// <summary>
	/// Starting mana
	/// </summary>
	public float maxMana = 20.0f;

	/// <summary>
	/// The mana recovey rate per second.
	/// </summary>
	public float manaRecoveyRate = 1.0f;

	public float health;

	public float mana;

	public bool destroyOnDeath = true;

	public bool collectibleOnDeath = false;

	public Transform collectible;

	void Start(){
		health = maxHealth;
		mana   = maxMana;
	}

	// Update is called once per frame
	void Update () {

		mana = Mathf.Min (maxMana, mana + manaRecoveyRate * Time.deltaTime);

		// If health is less or equal than zero, do death animation, for example, and destroy object
		if (health > 0.0f)
			return;

		// To Do
		// Do death animation, for example

		// Destroy object
		if (destroyOnDeath) {

			if (collectibleOnDeath)
				Instantiate (collectible, transform.position, Quaternion.identity);
			
			Destroy (gameObject);
		}
	}

	public bool HasMana (float amount){
		return (amount <= mana) ? true : false;
	}

	public void DecreaseMana (float decrease){
		
		mana -= decrease;

		mana = Mathf.Max (0.0f, Mathf.Min (mana, maxMana));
	}

	public void TakeDamage (float damage){

		// Reduce from health the value of damage
		// If damage is negative, life will increase
		health -= damage;

		health = Mathf.Max (0.0f, Mathf.Min (health, maxHealth));
	}
}
