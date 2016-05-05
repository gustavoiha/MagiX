using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject sliderS;
    public GameObject hist;
    public GameObject hist2;
    public Button startG;
    public Button b1;
    public Button b2;
    public Text title;

    public void NewGameButton()
    {
        Slider slider = sliderS.GetComponent<Slider>();
        slider.interactable = true;
        slider.value = 1;
        SetAlpha(b1, 0);
        SetAlpha(b2, 0);
        SetAlpha(title, 0);
    }
    public void StartGame()
    {
        GameController.startPhase("RebelVillage");
    }
	public void ExitGame ()
	{
		Application.Quit();
	}

    public void Update()
    {
        Slider slider = sliderS.GetComponent<Slider>();
        int v = Mathf.FloorToInt(slider.value);
        switch (v)
        {
            case 1:
                SetAlpha(hist.GetComponent<Text>(), 1);
                hist.GetComponent<Text>().text = "Long ago, we were told tales about our people. Tales of when we were free. Tales of when our people was happy. But today, these are but tales. Our people has been enslaved by one stranger who came and was able to use our ancient artifact. Blinded by our faith, we started following this stranger who should be, by our ancient legends, our saviour. But reality had a different story to tell.";
                SetAlpha(hist2.GetComponent<Text>(), 0);
                SetAlpha(startG, 0);
                break;
            case 2:
                SetAlpha(hist.GetComponent<Text>(), 1);
                hist.GetComponent<Text>().text = "The strange saviour started demanding our resources in exchange for our salvation. At first, we didn’t argue and we did everything we could. But at one point, we started to suffer a lack of resources for ourselves, and we tried to argue with him. Only to find out the truth. He didn’t care for us, only for our resources. We tried to fight against him, but with our artifact, he became too strong. We lost. We failed. He made us slaves to his quest of consuming our planet’s resources.";
                SetAlpha(hist2.GetComponent<Text>(), 0);
                SetAlpha(startG, 0);
                break;
            case 3:
                SetAlpha(hist.GetComponent<Text>(), 0);
                SetAlpha(hist2.GetComponent<Text>(), 1);
                SetAlpha(startG, 1);
                break;
            default:
                SetAlpha(hist.GetComponent<Text>(), 0);
                SetAlpha(hist2.GetComponent<Text>(), 0);
                SetAlpha(startG, 0);
                break;
        }
    }

    public void SetAlpha(Text gui, float alpha)
    {
        gui.color = new Color(gui.color.r, gui.color.g, gui.color.b, alpha);
    }
    public void SetAlpha(Button gui, float alpha)
    {
        gui.image.color = new Color(gui.image.color.r, gui.image.color.g, gui.image.color.b, alpha);
        gui.GetComponentInChildren<Text>().color = new Color(gui.GetComponentInChildren<Text>().color.r, gui.GetComponentInChildren<Text>().color.g, gui.GetComponentInChildren<Text>().color.b, alpha);
    }
}
