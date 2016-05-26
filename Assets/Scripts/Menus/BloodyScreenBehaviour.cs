using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BloodyScreenBehaviour : MonoBehaviour {

	private HealthController healthController;
	private Image image;

	public float percentThreshold = 50.0f;

	private float percent;

	// Use this for initialization
	void Start () {
		healthController = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthController> ();
		image = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		percent = healthController.health / healthController.maxHealth;

		Vector4 colorTemp = image.color;
		colorTemp.w = (1.0f - ((percent <= percentThreshold) ? percent / percentThreshold: 1.0f));

		image.color = colorTemp;

	}
}
