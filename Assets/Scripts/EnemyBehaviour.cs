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
	    
	}

    void OnMouseOver()
    {
        if (Input.GetKeyDown(key1))
        {
            player.GetComponent<SkillsController>().target = gameObject;
        }
    }
}
