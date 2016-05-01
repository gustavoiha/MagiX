using UnityEngine;
using System.Collections;

public class PlayerAnimatorEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void UseSkill(float a, int skillID){
		Debug.Log ("using skill: " + skillID);
	}
}
