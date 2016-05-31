using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarController : MonoBehaviour {

	private Image image;

	private float percent = 1.0f;

	public string point = "";

	public string character = "";

	public GameObject DeadPanel;

	public GameObject BossPanel;

	public GameObject WinPanel;

	private HealthController playerHealthController;
	private HealthController bossHealthController;
	private SoundManager  soundManager;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		playerHealthController = GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthController> ();
		soundManager =  GameObject.FindGameObjectWithTag ("Sound").GetComponent<SoundManager> ();
		DeadPanel.SetActive (false);
		WinPanel. SetActive (false);
		Time.timeScale = 1;
	}

	void Update (){

		if (character == "player") {
			if (playerHealthController == null)
				return;

			if (point == "health")
				percent = playerHealthController.health / playerHealthController.maxHealth;
			else if (point == "mana")
				percent = playerHealthController.mana / playerHealthController.maxMana;

			image.fillAmount = percent;
		}
		else if (character == "boss"){
			
			if (bossHealthController == null)
				if (GameObject.FindGameObjectWithTag ("Boss") != null)
					bossHealthController = GameObject.FindGameObjectWithTag ("Boss").GetComponent<HealthController> ();
			
			if (bossHealthController == null)
				return;
			
			percent = bossHealthController.health / bossHealthController.maxHealth;

			if (percent <= 0.0f) {
				GameManager.levelCleared = true;
				WinPanel.SetActive (true);
				Cursor.visible = true;
				Time.timeScale = 0.0f;
			}

			image.fillAmount = percent;
		}

	}

	public void SetDeadMenuState (bool state){
		DeadPanel.SetActive(state);
		soundManager.PlaySound (0);
	}

	public void SetBossMenuState (bool state){
		BossPanel.SetActive (state);
	}

	public void SetWinMenuState (bool state){
		WinPanel.SetActive (state);
	}

}
