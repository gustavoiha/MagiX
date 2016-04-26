using UnityEngine;
using System.Collections;

public class OgreProjectorController : MonoBehaviour {

	private Projector projector;

	// Use this for initialization
	void Start () {
		projector = gameObject.GetComponent<Projector> ();
		setState (false);
	}

	public void setState(bool newState){
		projector.enabled = newState;
	}

}
