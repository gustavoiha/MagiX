using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour {

    public GameObject guide;
    public static bool gotObject = false;

	void OnTriggerEnter(Collider collider){
		GameObject collisionObject = collider.gameObject;

		if (collisionObject.tag.Equals ("Player")) {
			GameController.aquiredItem ();
			Destroy (gameObject.transform.parent.gameObject);
			GameObject clone = Instantiate(guide, transform.position, Quaternion.LookRotation(Vector3.forward)) as GameObject;
            //Destroy(clone, 10);
			//clone.GetComponent<NovaBehaviour> ().ShowNextText ();
		}
	}

}
