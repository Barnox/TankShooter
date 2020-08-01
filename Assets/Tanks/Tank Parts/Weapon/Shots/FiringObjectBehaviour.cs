using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringObjectBehaviour : WeaponShotScript
{
    int remainingBarrage;
    float timeSinceBarrage;
    public GameObject parentWeapon;
    GameObject newShot;
    Vector3 inaccuracy;

    public void Start()
    {
        remainingBarrage = (int)barrageCount;
        timeSinceBarrage = barrageDelay;

        //this.transform.parent = parentWeapon.transform;
        //this.transform.localPosition = firingOffset;
        //this.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    public void Update()
    {
        if (remainingBarrage > 0) {
            if (timeSinceBarrage >= barrageDelay)
            {
                FireBarrage();
                timeSinceBarrage = 0;
                remainingBarrage -= 1;
            }
            else { timeSinceBarrage += Time.deltaTime; }
        }
        else
        { GameObject.Destroy(this.gameObject); }
    }

    public void FireBarrage()
    {
        for (int i = 0; i < barrageSize; i++)
        {

            newShot = Instantiate(fireResult,transform);
            newShot.transform.SetParent(null);
            inaccuracy = new Vector3(UnityEngine.Random.Range(-weaponAccuracy, weaponAccuracy), UnityEngine.Random.Range(-weaponAccuracy, weaponAccuracy), UnityEngine.Random.Range(-weaponAccuracy, weaponAccuracy));
            newShot.transform.Rotate(inaccuracy);
            WeaponShotScript newScript = (WeaponShotScript)newShot.AddComponent(fireScript.GetType());

            newScript.shotTarget = shotTarget;
            newScript.shotLifetime = shotLifetime;
            newScript.shotVelocity = shotVelocity;
            newScript.maxRange = maxRange;
            newScript.standardDamage = standardDamage;

        }

    }

}
