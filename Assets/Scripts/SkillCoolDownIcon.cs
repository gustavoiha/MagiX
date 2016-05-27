using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCoolDownIcon : MonoBehaviour {

	public int skillID;

	private Image image;

	private SkillsController skillsController;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		skillsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<SkillsController> ();
	}
	
	// Update is called once per frame
	void Update () {
		image.fillAmount = skillsController.TimeTilNext [skillID] / skillsController.coolDown [skillID];
	}
}
