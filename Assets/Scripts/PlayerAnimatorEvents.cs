using UnityEngine;
using System.Collections;

public class PlayerAnimatorEvents : MonoBehaviour {

	private FootStepsController footStepsController;

	void Start (){
		footStepsController = gameObject.GetComponent<FootStepsController> ();
	}

	public void EventUseSkill (int skillID){
		gameObject.GetComponent<SkillsController> ().UseSkill (skillID);
	}

	public void EventDead (int a){
		GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<ProgressBarController> ().SetDeadMenuState (true);
		Time.timeScale = 0.0f;
	}

	public void PlayFootstep (int a){
		footStepsController.PlayNextStep ();
	}
}
