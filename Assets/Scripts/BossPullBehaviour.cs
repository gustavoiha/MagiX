using UnityEngine;
using System.Collections;

public class BossPullBehaviour : MonoBehaviour {

    public Transform boss;

    public ForceMode forceMode;

    public float maxRadius = 90.0f; //Max radius of the sphere
    public float scale = 1.0f; //Used to expand the sphere
    public float growthRate = 1.0f; //Rate the sphere will expand
    public float radius = 1;//Radius of the implosion
    public float implosionForce = 90.0f; //Force of the implosion
    public float rotationSpeed; //Speed the sphere will rotate

    public float timeLeft = 2.0f; //Time between the sphere is fully grown and the implosion happens
	
    void Start() {
        transform.position = boss.position;
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, rotationSpeed) * Time.deltaTime); //Rotates the sphere
	    if(transform.localScale.y <= maxRadius) {
            transform.localScale = Vector3.one * scale; //Makes the sphere grow according to the scale
            scale += growthRate * Time.deltaTime; //Rises the scale according to the growthRate
            radius = transform.localScale.y; //Updates the radius
            print(radius);
            return; //Blocks the rest of the Update part (so it won't detonate)
        }
        timeLeft -= Time.deltaTime; //Starts countdown
        if (timeLeft < 0)
            Implode(); //Implodes when it finishes
     }

    void Implode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); //Checks everything around the sphere

        foreach (Collider hit in colliders)
        {

            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(-implosionForce, transform.position, radius, 1.0f, forceMode); //Implodes for every rigidbody close to the sphere
                //Debug.Log ("Added force!");
            }
        }
        Destroy(gameObject); //Destroys the sphere
    }
}
