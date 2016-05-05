using UnityEngine;
using System.Collections;

public class DarkRingBehaviour : MonoBehaviour {

    public float scale = 1.0f; //Used to expand the ring
    public float growthRate = 1.0f; //Rate the ring will expand
    public float damage = 5.0f; //Damage the ring will do
    public float maxRate = 80; //Max radius for the ring to grow 

    // Use this for initialization
    void Start () {
		transform.position += Vector3.up * 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(1, 0, 1) * scale; //Makes the ring grow according to the scale
        scale += growthRate * Time.deltaTime; //Rises the scale according to time
        if (transform.localScale.x >= maxRate)
            Destroy(gameObject); //When it reaches a max radius, it is destroyed;
    }
}
