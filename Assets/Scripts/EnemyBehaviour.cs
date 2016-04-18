using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public KeyCode key1;

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        key1 = KeyCode.Mouse1;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<SkillsController>().target == gameObject)
            GetComponentInChildren<ParticleSystem>().Play();
        else
            GetComponentInChildren<ParticleSystem>().Stop();
    }

    void OnMouseOver()
    {
        if (Input.GetKeyDown(key1))
            player.GetComponent<SkillsController>().target = gameObject;
        
        
    }
}
