using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarTracker : MonoBehaviour {

	private HealthController healthController;
	private Image image;

	// Use this for initialization
	void Start () {
		healthController = transform.root.gameObject.GetComponent<HealthController> ();
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		image.fillAmount = healthController.health / healthController.maxHealth;
	}
}
