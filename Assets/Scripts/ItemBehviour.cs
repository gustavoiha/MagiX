using UnityEngine;
using System.Collections;

public class ItemBehviour : MonoBehaviour {

    public GameObject guide;
    public static bool gotObject;

    void Update()
    {
        gotObject = false;
    }

	void OnTriggerEnter(Collider collider){
		GameObject collisionObject = collider.gameObject;

		if (collisionObject.tag.Equals ("Player")) {
			GameController.aquiredItem ();
			Destroy (gameObject.transform.parent.gameObject);
            Object clone = Instantiate(guide, transform.position, Quaternion.LookRotation(Vector3.forward));
            Destroy(clone, 10);
            gotObject = true;
		}
	}

}
