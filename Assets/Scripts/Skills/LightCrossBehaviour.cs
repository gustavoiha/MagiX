using UnityEngine;
using System.Collections;

public class LightCrossBehaviour : MonoBehaviour {

	//Prefab dos raios
    public GameObject lightRay; 

	//Localização do player
    public Transform player;

	// Angle interval to fire multiple rays
	public float fireAngle = 15.0f;
	
	// Update is called once per frame
	void Update () {

		//Adquire a rotação do player
        Quaternion rotation = Quaternion.LookRotation(new Vector3(player.position.x, 0, player.position.z), Vector3.forward); 

		//Cria os raios a partir da rotação do player
        CreateLightRay(rotation);

		//Destroi esfera
        Destroy(gameObject); 
	}

	/// <summary>
	/// Cria raios rotacionando eles a cada "angle" graus.
	/// </summary>
	/// <param name="rotation">Rotation.</param>
	/// <param name="angle">Angle.</param>
	void CreateLightRay(Quaternion rotation, float angleInterval) {

		// Subtract multiples of 360 degrees
		angleInterval = angleInterval % 360.0f;

		// Angle to fire
		float angle = 0.0f;

		while (angle <= 360.0f) {
			Instantiate (lightRay, transform.position, rotation * Quaternion.Euler (angle, 0, 0));
			angle += angleInterval;
		}
    }
}
