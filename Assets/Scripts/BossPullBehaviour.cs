using UnityEngine;
using System.Collections;

public class BossPullBehaviour : MonoBehaviour {

    public GameObject boss;

    public ForceMode forceMode;

    public float maxRadius = 90.0f; //Max radius of the sphere
    public float scale = 1.0f; //Used to expand the sphere
    public float growthRate = 1.0f; //Rate the sphere will expand
    public float radius = 1;//Radius of the implosion
    public float implosionForce = 90.0f; //Force of the implosion
    public float rotationSpeed; //Speed the sphere will rotate

    public float timeLeft = 2.0f; //Time between the sphere is fully grown and the implosion happens
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, rotationSpeed) * Time.deltaTime);
	    if(transform.localScale.y <= maxRadius)
        {
            transform.localScale = Vector3.one * scale; //Faz a esfera crescer de acordo com o scale
            scale += growthRate * Time.deltaTime; //Aumenta o scale conforme o tempo passa
            radius = transform.localScale.y;
            return;
        }
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
            Implode();
     }

    void Implode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in colliders)
        {

            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(-implosionForce, transform.position, radius, 1.0f, forceMode);
                //Debug.Log ("Added force!");
            }
        }

        //Destrói a esfera
        Destroy(gameObject);
    }
}
