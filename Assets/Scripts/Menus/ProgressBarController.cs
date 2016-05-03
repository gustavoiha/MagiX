using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarController : MonoBehaviour {

	private Image image;

	private float percent = 1.0f;

	public string point = "";

	public GameObject DeadPanel;

	private HealthController healthController;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		healthController = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthController> ();
		DeadPanel.SetActive(false);
		Time.timeScale = 1;
	}

	void Update(){

		if (healthController == null)
			return;

		if (point == "health")
			percent = healthController.health / healthController.maxHealth;
		else if (point == "mana")
			percent = healthController.mana / healthController.maxMana;

		image.fillAmount = percent;

		if (image.fillAmount == 0)
		{
			DeadPanel.SetActive(true);
			Time.timeScale = 0;

		}

	}

}
