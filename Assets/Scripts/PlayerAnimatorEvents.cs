using UnityEngine;
using System.Collections;

public class PlayerAnimatorEvents : MonoBehaviour {

	private FootStepsController footStepsController;
	private SkillsController skillsController;

	void Start (){
		footStepsController = gameObject.GetComponent<FootStepsController> ();
		skillsController 	= gameObject.GetComponent<SkillsController> ();
	}

	public void EventUseSkill (int skillID){
		skillsController.UseSkill (skillID);
	}

	public void EndSkill (int skillID){
		skillsController.EndedSkill (skillID);
	}

	public void EventDead (int a){
		GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<ProgressBarController> ().SetDeadMenuState (true);
		Cursor.visible = true;
		Time.timeScale = 0.0f;
	}

	public void PlayFootstep (int a){
		footStepsController.PlayNextStep ();
	}

	public void DoMeeleDamage (int a){
		gameObject.GetComponentInChildren<DoDamage> ().enabled = true;
	}
}
