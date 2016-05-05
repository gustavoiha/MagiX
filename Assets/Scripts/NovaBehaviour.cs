using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NovaBehaviour : MonoBehaviour {

    public Text text;

    void Start() {
        text.text = "We have many lives to save.";
        SetAlpha(text, 1);
        text.CrossFadeAlpha(0, 5, false);
    }

	// Update is called once per frame
	void Update () {
        if(ItemBehviour.gotObject)
            switch (GameController.itensLeft)
            {
                case 4:
                    text.text = "Your journey has just begun, child. May the light guide your path.";
                    text.CrossFadeAlpha(1, 0, false);
                    text.CrossFadeAlpha(0, 10, false);
                    break;
                case 3:
                    text.text = "This place was so beautiful before the tragedy... Let's make it beautiful once more, shall we?";
                    text.CrossFadeAlpha(1, 0, false);
                    text.CrossFadeAlpha(0, 10, false);
                    break;
                case 2:
                    text.text = "We're getting there, my dearest. Follow the light and it shall guide you to the truth.";
                    text.CrossFadeAlpha(1, 0, false);
                    text.CrossFadeAlpha(0, 10, false);
                    break;
                case 1:
                    text.text = "One more to go! You can do it! Let's destroy this darkness once and for all!";
                    text.CrossFadeAlpha(1, 0, false);
                    text.CrossFadeAlpha(0, 10, false);
                    break;
                case 0:
                    text.text = "Excellent, my child! You may know face your enemy! May the light always shine brighter at you and vanquish this evil once and for all!";
                    text.CrossFadeAlpha(1, 0, false);
                    text.CrossFadeAlpha(0, 10, false);
                    break;
                default:
                    break;
            }
	}

    public void SetAlpha(Text gui, float alpha)
    {
        gui.color = new Color(gui.color.r, gui.color.g, gui.color.b, alpha);
    }
}
