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
		// Do death anmation, for example

		// Destroy object
		Destroy (gameObject);
	}

	public bool HasMana (float amount){
		return (amount <= mana) ? true : false;
	}

	public void DecreaseMana (float decrease){
		
		mana -= decrease;

		if (mana < 0.0f)
			mana = 0.0f;
	}

	public void TakeDamage (float damage){

		// Reduce from health the value of damage
		// If damage is negative, life will increase
		health -= damage;

		if (health < 0.0f)
			health = 0.0f;
	}
}
