using UnityEngine;
using System.Collections;

public class LightCrossBehaviour : MonoBehaviour {

	//Prefab dos raios
	public GameObject lightRay; 

	//Localização do player
    public Transform player;

	// Angle interval to fire multiple rays
	public float fireAngle = 15.0f;

	// Spacing between lightRay and player
	public float fireSpacing = 10.0f;

	// Time lightCross starting ball will exist
	public float lightCrossTimeToDeath = 1.0f;

	public Quaternion startingRotation;

	void Start(){

        //Finds the player
        player = GameObject.FindGameObjectWithTag("Player").transform;
		
        //Adquire a rotação do player
		//Quaternion rotation = Quaternion.LookRotation(new Vector3(player.position.x, 0, player.position.z), Vector3.forward); 

		//Cria os raios a partir da rotação do player
		CreateLightRays(fireAngle);

		//Destroi esfera
		Destroy(gameObject, lightCrossTimeToDeath); 

		startingRotation = transform.rotation;
	}

	void Update(){

		// Lock rotation
		transform.rotation = startingRotation;
	}

	/// <summary>
	/// Cria raios rotacionando eles a cada "angle" graus.
	/// </summary>
	/// <param name="rotation">Rotation.</param>
	/// <param name="angle">Angle.</param>
	private void CreateLightRays(float angleInterval) {

		// Subtract multiples of 360 degrees
		angleInterval = angleInterval % 360.0f;

		// Angle to fire
		float angle = 0.0f;

		Vector3 firePosition = transform.position;

		while (angle <= 360.0f) {
			
			// Instantiate lightRay transform and store it in newLightRay
			GameObject newLightRay = Instantiate (lightRay, gameObject.transform.position, transform.rotation * Quaternion.Euler (90, angle, 0)) as GameObject;

			newLightRay.transform.parent = gameObject.transform;

			newLightRay.transform.position -= newLightRay.transform.up * fireSpacing;

			angle += angleInterval;
		}
    }
}
