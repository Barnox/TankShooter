using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShotBehaviour : WeaponShotScript
{
    float launchAngle = 30;
    float homingDelay = .2f;
    float homingFrequency = 0.05f;
    float homingAngle = 10;
    bool isEntityHoming = true;
    Transform entityHoming;

    Vector3 vectorToTarget;

    // Start is called before the first frame update
    void Start()
    {

        Destroy(this.gameObject, shotLifetime);
        transform.forward = Vector3.RotateTowards(transform.forward, Vector3.up, launchAngle * Mathf.Deg2Rad, 0);
        InvokeRepeating("TurnTowardsTarget", homingDelay, homingFrequency);

        if (isEntityHoming == true)
        { entityHoming = (Physics.OverlapSphere(shotTarget, .5f))[0].transform; }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * shotVelocity * Time.deltaTime;
    }

    void TurnTowardsTarget()
    {
        if (entityHoming) { shotTarget = entityHoming.position; }
        vectorToTarget = shotTarget - transform.position;
        transform.forward = Vector3.RotateTowards(transform.forward, vectorToTarget, homingAngle * Mathf.Deg2Rad, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.transform.name);
        if (other.transform.parent != null)
        {
            if (other.gameObject.transform.parent.GetComponent<ITakesDamage>() != null)
            {
                other.gameObject.transform.parent.GetComponent<ITakesDamage>().Damage(standardDamage);
            }
        }

        Destroy(gameObject);

    }


}

