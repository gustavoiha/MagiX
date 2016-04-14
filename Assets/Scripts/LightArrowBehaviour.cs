using UnityEngine;
using System.Collections;

public class LightArrowBehaviour : MonoBehaviour {

    public int damage = 3;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
       //if(other.gameObject.tag == "Enemy")
        {
            //Mob enemy = other.GetComponent<Mob>();
            //enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
