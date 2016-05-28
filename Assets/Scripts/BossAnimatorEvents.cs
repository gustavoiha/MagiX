using UnityEngine;
using System.Collections;

public class BossAnimatorEvents : MonoBehaviour {

	public void PlaySound (int sound){
		transform.parent.gameObject.GetComponent<SoundManager> ().PlaySound (sound);
	}

	public void setStormSkillState (int i){
		transform.parent.gameObject.GetComponent<BossBehaviour> ().setStormSkillState (i);
	}

	public void setSwordTrail (int i){
		transform.parent.gameObject.GetComponent<BossBehaviour> ().SetSwordTrail ((i >= 0) ? true : false);
	}

	public void usingSkill (int i){
		transform.parent.gameObject.GetComponent<BossBehaviour> ().UsingSkill (i);
	}

	public void EnableDamage (int i){
		transform.parent.gameObject.GetComponentInChildren<DoDamage> ().enabled = true;
	}

	public void PlayNextSlash (int i){
		transform.parent.gameObject.GetComponent<SoundAlternate> ().PlayNextSound ();
	}
}
