using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCChatBehaviour : MonoBehaviour {

	public Text displayText;

	public string[] chatTexts;

	private Text chatKey;

	public KeyCode keyCode;

	public string chatKeyString;

	public string tagsToActivateChat;

	private bool isNear;

	public float chatDuration = 8.0f;

	void Start (){
		chatKey = GetComponentInChildren<Text> ();
	}

	void OnTriggerEnter (Collider collider){

		if (!tagsToActivateChat.Contains (collider.tag))
			return;

		chatKey.text = chatKeyString;

		isNear = true;
	}

	void Update (){
		
		if (!isNear)
			return;

		if (Input.GetKeyUp (keyCode))
			ShowHint ();
	}

	void OnTriggerExit (Collider collider){

		if (!tagsToActivateChat.Contains (collider.tag))
			return;

		chatKey.text = "";

		isNear = false;
	}

	private void ShowHint (){
		displayText.text = chatTexts [GameController.totalItensToGet - GameController.itensLeft];
		displayText.CrossFadeAlpha(1, 0, false);
		displayText.CrossFadeAlpha(0, chatDuration, false);
	}
}
