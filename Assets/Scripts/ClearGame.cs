using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearGame : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject highScoreText;

    private FadeEffect fadeEffect;

    // Use this for initialization
    void Start()
    {
		// Coloca o highscore ai, se quiser!
        //highScoreText.GetComponent<Text>().text = "Highscore: " + highscore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}