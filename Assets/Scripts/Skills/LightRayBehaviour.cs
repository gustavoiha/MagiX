using UnityEngine;
using System.Collections;

public class LightRayBehaviour : MonoBehaviour {

	// Scale to multiply per second
    public float growthRate = 200.0f;

	// max scale permitted
	public float maxScale = 200.0f;

	// Starting scale
    private Vector3 startingScale;

	// Starting length
	//private float startingLength;

	// Starting position
	//private Vector3 startingPosition;

	//public float damage = 0.5f;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");

		//Destroy(gameObject, 1.0f); //Auto-destruição -- Update: lightRay is child of LightCross now, which already self-destructs.

		startingScale    = transform.localScale; //Escala atual local do raio
		//startingLength   = gameObject.GetComponent<MeshRenderer>().bounds.size.z;
		//startingPosition = gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.localScale.y >= maxScale)
			return;

		Vector3 newScale = startingScale;
		newScale.y = transform.localScale.y + growthRate * Time.deltaTime;

		transform.localScale = newScale; //Aumenta esse float de acordo com a constante

		// Move object so it doesn't cross player when scaling
		//transform.localPosition = startingPosition + (getLength() - startingLength) / 2.0f * transform.up;

	}

    /*void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<HealthController>().TakeDamage(damage);
        }
    }*/

	private float getLength(){
		return gameObject.GetComponent<MeshRenderer>().bounds.size.z;
	}
}
