using UnityEngine;
using System.Collections;

public class DarkRingBehaviour : MonoBehaviour {

	private float scale = 1.0f; //Used to expand the ring
    public float growthRate = 1.0f; //Rate the ring will expand
    public float maxRadius = 80; //Max radius for the ring to grow 

    // Use this for initialization
    void Start () {
		transform.position += Vector3.up * 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.one * scale; //Makes the ring grow according to the scale
        scale += growthRate * Time.deltaTime; //Rises the scale according to time
		if (transform.localScale.x >= maxRadius)
            Destroy(gameObject); //When it reaches a max radius, it is destroyed;
    }
}
