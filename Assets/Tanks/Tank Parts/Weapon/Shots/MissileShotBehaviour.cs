using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShotBehaviour : WeaponShotScript
{
    float launchAngle = 30;
    float homingDelay = .5f;
    float homingFrequency = 0.05f;
    float homingAngle = 2;
    bool isEntityHoming = true;
    Transform entityHoming;
    LayerMask maskPlayerEnemy;
    Collider[] entityTarget;

    BoxCollider ownCollider;

    Vector3 vectorToTarget;

    // Start is called before the first frame update
    void Start()
    {

        Destroy(this.gameObject, shotLifetime);
        transform.forward = Vector3.RotateTowards(transform.forward, Vector3.up, launchAngle * Mathf.Deg2Rad, 0);
        InvokeRepeating("TurnTowardsTarget", homingDelay, homingFrequency);
        Invoke("EnableCollider", homingDelay / 2);



        if (isEntityHoming == true)
        {
            maskPlayerEnemy = LayerMask.GetMask("Player", "Enemy");
            entityTarget = Physics.OverlapSphere(shotTarget, .5f, maskPlayerEnemy);
            if (entityTarget.Length != 0) { entityHoming = entityTarget[0].transform; } 
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * shotVelocity * Time.deltaTime;
    }

    void TurnTowardsTarget()
    {
        if (entityHoming) { shotTarget = entityHoming.position + Vector3.up; }
        vectorToTarget = shotTarget - transform.position;
        transform.forward = Vector3.RotateTowards(transform.forward, vectorToTarget, homingAngle * Mathf.Deg2Rad, 0);
        transform.forward = Vector3.RotateTowards(transform.forward, Vector3.down, 1 * Mathf.Deg2Rad, 0);
        transform.forward = Vector3.RotateTowards(transform.forward, new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)), 10 * Mathf.Deg2Rad, 0);
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

    void EnableCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }


}

