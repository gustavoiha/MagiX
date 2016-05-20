using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OgreProjectorController : MonoBehaviour {

	private Projector projector;
	private Canvas healthBarCanvas;

	// Use this for initialization
	void Start () {
		projector = gameObject.GetComponent<Projector> ();
		healthBarCanvas = transform.root.gameObject.GetComponentInChildren<Canvas> ();
		setState (false);
	}

	public void setState(bool newState){
		if (projector != null)
			projector.enabled = newState;

		if (healthBarCanvas != null)
			healthBarCanvas.enabled = newState;
	}

}