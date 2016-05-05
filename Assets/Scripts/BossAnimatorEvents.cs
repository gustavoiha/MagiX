using UnityEngine;
using System.Collections;

public class BossAnimatorEvents : MonoBehaviour {

	public void setStormSkillState (int i){
		transform.parent.gameObject.GetComponent<BossBehaviour> ().setStormSkillState (i);
	}

	public void setSwordTrail (int i){
		transform.parent.gameObject.GetComponent<BossBehaviour> ().SetSwordTrail ((i >= 0) ? true : false);
	}

	public void usingSkill (int i){
		transform.parent.gameObject.GetComponent<BossBehaviour> ().UsingSkill (i);
	}
}
