using UnityEngine;
using System.Collections;

public class OgreAnimatorEvents : MonoBehaviour {

	void DoDamage (){
		transform.parent.gameObject.GetComponentInChildren<DoDamage> ().enabled = true;
	}
}
