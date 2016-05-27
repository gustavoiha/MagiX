using UnityEngine;
using System.Collections;

public class DefenceDomeBehaviour : MonoBehaviour {

    public int y = 90;

	public float duration = 8.0f;

	private HealthController healthController;

	void Start(){
		Destroy(gameObject, duration);

		healthController = transform.root.gameObject.GetComponent<HealthController> ();
		healthController.Invincible = true;
	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, y, 0) * Time.deltaTime);
	}

	void OnDestroy () {
		healthController.Invincible = false;
	}
}