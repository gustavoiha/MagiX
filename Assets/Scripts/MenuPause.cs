using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuPause : MonoBehaviour {

	public Text soundText;
	public Text qualityText;
	public GameObject optionsPanel;
	public GameObject pausePanel;
	public GameObject pauseselectionPanel;

	// Use this for initialization
	void Start () {
	
		pausePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape"))
		{
			if(pausePanel.activeSelf == false)
			{
				pausePanel.SetActive(true);
				pauseselectionPanel.SetActive(true);
				optionsPanel.SetActive(false);

				Time.timeScale = 0;
			}
			else
			{ 
				pausePanel.SetActive(false);
				Time.timeScale = 1;
			}
		}
			
		//soundText.text = "Master Volume - (" + AudioListener.volume.ToString ("f2") + ")";

		int currentQuality = QualitySettings.GetQualityLevel ();
		string qualityName = QualitySettings.names [currentQuality];
		qualityText.text = "Quality: "+qualityName;
	}

	public void ResumeGame()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}

	public void ShowOptions()
	{
		pauseselectionPanel.SetActive(false);
		optionsPanel.SetActive(true);
	}

	public void BackButton()
	{
		pauseselectionPanel.SetActive(true);
		optionsPanel.SetActive(false);
	}
	public void DecreaseButton(){
		int currentQuality = QualitySettings.GetQualityLevel ();
		string qualityName = QualitySettings.names [currentQuality];
		QualitySettings.DecreaseLevel ();
	}
	public void IncreaseButton(){
		int currentQuality = QualitySettings.GetQualityLevel ();
		string qualityName = QualitySettings.names [currentQuality];
		QualitySettings.IncreaseLevel();
	}
	public void MainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void ExitGame(){
		Application.Quit();
	}
}
