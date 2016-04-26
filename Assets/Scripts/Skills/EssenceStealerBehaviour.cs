using UnityEngine;
using System.Collections;

public class EssenceStealerBehaviour : MonoBehaviour {

    //ParticleSystem
    ParticleSystem ps;

    //GameObjects
    GameObject player;
    GameObject target;

    public float damage = 5f; //Damage

	// Use this for initialization
	void Start () {
        //Get ParticleSystem attached to the prefab
        ps = GetComponent<ParticleSystem>();
        //Find the player with the tag
        player = GameObject.FindGameObjectWithTag("Player");
        //Using the player's script, get their target
        //target = player.GetComponent<SkillsController>().target;
        //Starts to play the Particle System
        ps.Play();
	}
	
	// Update is called once per frame
	void Update () {
        //When the player stops pressing the key, the effect is gone
        if (Input.GetKeyUp(player.GetComponent<SkillsController>().magicFive) || target == null) {
            ps.Stop();
            Destroy(gameObject, 2.0f);
            return;
        }
        //Damages the target.
        target.GetComponent<HealthController>().TakeDamage(damage * Time.deltaTime);
        //Cura aqui

        //Updates new Rotation to face the player
        transform.rotation = Quaternion.LookRotation(player.transform.position-target.transform.position);
        //Updates location of the target
        transform.position = target.transform.position;
	}
}
