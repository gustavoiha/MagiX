using UnityEngine;
using System.Collections;

public class MagicUser : MonoBehaviour {

    public GameObject lightArrow;
    public GameObject lightBall;

    public float lightArrowForce = 100.0f;
    public float lightBallForce = 50.0f;
    public float angleX = 90;
    public float angleY = 0;
    public float angleZ = 180;
    Vector3 direction;

    Vector3 location;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        location = transform.position + Vector3.forward*3;
        var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        direction = worldPosition - transform.position;
        direction.Normalize();
        if (Input.GetKey(KeyCode.Alpha1))
            UseLightArrow();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseLightBall();
	}

    void UseLightArrow()
    {
        GameObject projectile = Instantiate(lightArrow, location, Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 180)) as GameObject;
        projectile.GetComponent<Rigidbody>().useGravity = false;
        projectile.GetComponent<Rigidbody>().AddForce(direction * lightArrowForce);
    }
    void UseLightBall()
    {
        GameObject projectile = Instantiate(lightBall, location, Quaternion.LookRotation(direction) * Quaternion.Euler(angleX, angleY, angleZ)) as GameObject;
        projectile.GetComponent<Rigidbody>().useGravity = false;
        projectile.GetComponent<Rigidbody>().AddForce(direction * lightBallForce);
    }
}
