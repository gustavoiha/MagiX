using UnityEngine;
using System.Collections;

public class LightBallBehaviour : MonoBehaviour {

    public int damage = 3;
    public float scale = 1.0f;
    public float growthRate = 0.5f;
    public ParticleSystem particleS;
    bool detonation;
    bool detonateGoing = false;

    // Use this for initialization
    void Start()
    {
        detonation = false;
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (detonateGoing) return;

        if (!detonation) {
            transform.localScale = Vector3.one * scale;
            particleS.transform.localScale = Vector3.one * scale;
        }
        else
        {
            if(transform.localScale.x <= 0.0001)
            {
                Detonate();
                return;
            }
            transform.localScale = Vector3.one / (2 * scale);
        }
        scale += growthRate * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Enemy")
        {
            //Mob enemy = other.GetComponent<Mob>();
            //enemy.TakeDamage(damage);
            detonation = true;
        }
    }

    void Detonate()
    {
        detonateGoing = true;

    }
}
