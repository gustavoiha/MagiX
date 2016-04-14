using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	/// <summary>
	/// Starting health
	/// </summary>
	public float health = 10.0f;
	
	// Update is called once per frame
	void Update () {

		// If health is less or equal than zero, do death animation, for example, and destroy object
		if (health > 0.0f)
			return;

		// To Do
		// Do death anmation, for example

		// Destroy object
		Destroy (gameObject);
	}

	public void TakeDamage (float damage){

		// Reduce from health the absolute value of damage
		health -= Mathf.Abs (damage);
	}
}
