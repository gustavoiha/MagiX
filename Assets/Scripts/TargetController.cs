using UnityEngine;
using System.Collections;
using System;

public class TargetController : MonoBehaviour {

    public GameObject target = null;
    int switchT = 0;

    public bool HasTarget() {
        //Returns true if there is a target or false if there is none
        if (target == null)
            return false;
        else return true;
    }

    public GameObject GetTarget() {
        //Returns the target
        return target;
    }

    public GameObject[] PickNearbyTarget(float radiusMax) {
        GameObject[] targets;
        //Creates an array with all enemies
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        //Creates an array for close objects
        Collider[] colTargets = Physics.OverlapSphere(transform.position, radiusMax);
        //Compare enemies to close objects
        for (int i = 0; i < targets.Length; i++) {
            bool same = false;
            for (int o = 0; o < colTargets.Length; o++) {
                //If there's a match, same becomes true
                if (targets[i] == colTargets[o].gameObject) {
                    same = true;
                    break;
                }
            }
            //If there's no match, the enemy is removed from the array
            if (!same)
                targets[i] = null;
        }
        //Here we resize the enemy array, removing the null ones
        for (int i = 0; i < targets.Length; i++) {
            //If there's a null space
            if (targets[i] == null) {
                //We remove it by pulling all the array back by 1
                for (int a = i; a < targets.Length - 1; a++) {
                    targets[a] = targets[a + 1];
                }
                //When it ends, we reduce its size by one
                Array.Resize<GameObject>(ref targets, targets.Length - 1);
                //Check if the new one is also null;
                i--;
            }
        }

        return targets;
    }

    public bool TargetIsDead() {
        if (HasTarget())
            if (target.GetComponent<HealthController>().health <= 0)
                return true;
        return false;      
    }

    public void SwitchTarget(GameObject[] targets) {
        //In case we kill too many enemies and the current int is bigger than the new array, we reset it
        if (switchT >= targets.Length)
            switchT = 0;
        //When we press the key, we will change targets according to the array we created
        target = targets[switchT];
        //If we reach the maximum of the array, we reset the int
        if (switchT >= targets.Length - 1)
        {
            switchT = 0;
        }
        else //If we don't reach the end, we add one to it
            switchT++;
    }

    public GameObject FindNearTarget(float radius, float radiusMax){
        if (!HasTarget())
            while (!HasTarget())
            {
                //Create a globe around the character
                foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
                {
                    if (col.tag == "Enemy") // If there's an enemy around, we'll pick them as a target
                        target = col.gameObject;
                    radius += 0.1f;// Otherwise, we'll increase the globe until we find one.
                }
                if (radius > radiusMax) //If no enemy is found in 1000 units, we'll break the while loop
                {
                    print("No enemy around");
                    return null;
                }
            }
        return target;
    }
 
}
