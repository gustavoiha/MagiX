using UnityEngine;
using System.Collections;

public class FadeEffect : MonoBehaviour {
    public Texture2D fadeTexture;
    
    public delegate void OnFadeIn();
    public delegate void OnFadeOut();
    public OnFadeIn onFadeIn;
    public OnFadeOut onFadeOut;

    private float fadeTime;
    private float alpha;
    private float fadeDirection;
    private bool fading;
    

	// Use this for initialization
	void Start () {
        alpha = 0f;
        fading = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (fading)
        {
            alpha += fadeDirection * Time.deltaTime / fadeTime;
            alpha = Mathf.Clamp01(alpha);
            if (fadeDirection == -1 && alpha == 0f)
            {
                fading = false;
                if (onFadeIn != null)
                {
                    onFadeIn();
                }
            }
            else if (fadeDirection == 1 && alpha == 1f)
            {
                fading = false;
                if (onFadeOut != null)
                {
                    onFadeOut();
                }
            }
        }
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = -100;
        GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), fadeTexture);
    }

    public void FadeIn(float time)
    {
        alpha = 1f;
        fadeDirection = -1;
        fadeTime = time;
        fading = true;
    }

    public void FadeOut(float time)
    {
        alpha = 0f;
        fadeDirection = 1;
        fadeTime = time;
        fading = true;
    }
}
