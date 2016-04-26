using UnityEngine;
using System.Collections;

public class EssenceStealerBehaviour : MonoBehaviour {

    //ParticleSystem
    ParticleSystem ps;

    //GameObjects
    private GameObject player;
	private Transform target;
	private NewTargetController targetController;

    public float damage = 5f; //Damage

	// Use this for initialization
	void Start () {
        //Get ParticleSystem attached to the prefab
        ps = GetComponent<ParticleSystem>();

        //Find the player with the tag
        player = GameObject.FindGameObjectWithTag("Player");

		targetController = player.GetComponent<NewTargetController> ();
        //Using the player's script, get their target
        //target = player.GetComponent<SkillsController>().target;
        //Starts to play the Particle System
        ps.Play();
	}
	
	// Update is called once per frame
	void Update () {

		target = targetController.GetTargetTransform ();

        //When the player stops pressing the key, the effect is gone
        if (Input.GetKeyUp(player.GetComponent<SkillsController>().magicFive) || target == null) {
            ps.Stop();
            Destroy(gameObject, 2.0f);
            return;
        }

        //Damages the target.
        target.gameObject.GetComponent<HealthController>().TakeDamage(damage * Time.deltaTime);

        //Cura aqui
		player.GetComponent<HealthController> ().TakeDamage(-damage * Time.deltaTime);

        //Updates new Rotation to face the player
        transform.rotation = Quaternion.LookRotation(player.transform.position-target.position);
        //Updates location of the target
        transform.position = target.position;
	}
}
