using UnityEngine;
using System.Collections;

public class ItemBehviour : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		GameObject collisionObject = collider.gameObject;

		if (collisionObject.tag.Equals ("Player")) {
			GameController.aquiredItem ();
			Destroy (gameObject.transform.parent.gameObject);
		}
	}

}
