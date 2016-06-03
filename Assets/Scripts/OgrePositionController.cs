using UnityEngine;
using System.Collections;

// Ogros irão surgir no lugar das capsulas quando o player
// estiver a uma distância mínima das capsulas
public class OgrePositionController : MonoBehaviour {

	// Ogro é o objeto a ser instanciado
	public GameObject ogre; 

	private bool hasSpawned = false;

	void OnTriggerEnter (Collider collider) {
		if(collider.gameObject.CompareTag ("Player") && !hasSpawned) {
			Instantiate (ogre, gameObject.transform.position, gameObject.transform.rotation);
			hasSpawned = true;
			Destroy (gameObject);
		}
	}
}
