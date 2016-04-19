using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    //Keycode
    public KeyCode key1;

    //Player
    public GameObject player;

	// Use this for initialization
	void Start () {
        //Initially, the Keycode will be Right click on the mouse (May be changed)
        key1 = KeyCode.Mouse1;
        //Player will automatically find the player using their tag
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        //If the enemy is the target, particles will start to play so the player will know that
        if (player.GetComponent<SkillsController>().target == gameObject)
            GetComponentInChildren<ParticleSystem>().Play();
        else
            GetComponentInChildren<ParticleSystem>().Stop();
    }

    void OnMouseOver()
    {
        //When the player clicks on the enemy, the enemy will become their target
        if (Input.GetKeyDown(key1))
            player.GetComponent<SkillsController>().target = gameObject;        
    }
}
