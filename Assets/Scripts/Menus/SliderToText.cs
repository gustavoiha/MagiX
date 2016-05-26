using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SliderToText : MonoBehaviour {

	public string text;

	private string textToShow;

	public string numberPlacer;

	public float startingValue;

	private Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		AdjustValue (startingValue);
	}

	public void AdjustValue (float newValue){
		textToShow = text.Replace (numberPlacer, newValue.ToString ("F2"));

		myText.text = textToShow;
	}

}
