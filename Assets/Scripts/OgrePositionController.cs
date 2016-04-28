using UnityEngine;
using System.Collections;

// Ogros irão surgir no lugar das capsulas quando o player
// estiver a uma distância mínima das capsulas
public class OgrePositionController : MonoBehaviour {

	// Ogro é o objeto a ser instanciado
	public GameObject ogre; 


	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Player")) {
			Instantiate (ogre, gameObject.transform.position, gameObject.transform.rotation);
		}
	}
}
