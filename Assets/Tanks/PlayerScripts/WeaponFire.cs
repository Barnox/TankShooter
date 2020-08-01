using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    Vector3 fireOffset;
    GameObject fireObject;
    float refireRate;
    float sinceLastShot;
    int barrageSize;
    float barrageDelay;
    int barrageCount;
    float standardDamage;
    float weaponAccuracy;
    public float maxRange;
    float shotVelocity;
    float shotLifetime;
    TankyWeapon thisWeapon;
    WeaponShotScript fireScript;
    Type fireScriptType;
    Vector3 offsetPosition;
    GameObject firingObject;
    GameObject newShot;
    FiringObjectBehaviour newShotScript;
    



    // Start is called before the first frame update
    void Start()
    {
        thisWeapon = (TankyWeapon)GetComponent<TankyHealth>().ownPart;
        fireOffset = thisWeapon.firingOffset;
        refireRate = thisWeapon.weaponRateoffire;
        fireObject = thisWeapon.fireResult;
        maxRange = thisWeapon.maxRange;
        barrageSize = thisWeapon.barrageSize;
        barrageDelay = thisWeapon.barrageDelay;
        barrageCount = thisWeapon.barrageCount;
        standardDamage = thisWeapon.standardDamage;
        weaponAccuracy = thisWeapon.weaponAccuracy;
        shotLifetime = thisWeapon.shotLifetime;
        shotVelocity = thisWeapon.shotVelocity;
        firingObject = thisWeapon.firingObject;
        fireScript = thisWeapon.fireScript;
        fireScriptType = fireScript.GetType();
        

        sinceLastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        sinceLastShot += Time.deltaTime;
    }

    public void Fire(Vector3 targetPosition)
    {
        if (sinceLastShot >= refireRate)
        {
            Debug.Log("Fired");
            CreateShot();
            sinceLastShot = 0;
            PopulateShot(targetPosition);
        }



    }

    public void CreateShot()
    {
        offsetPosition = gameObject.transform.TransformPoint(fireOffset);
        newShot = Instantiate(firingObject, transform);
        newShot.transform.localPosition = fireOffset;
    }

    public void PopulateShot(Vector3 targetPosition)
    {
        newShotScript = newShot.GetComponent<FiringObjectBehaviour>();


        newShotScript.weaponAccuracy = weaponAccuracy;

        newShotScript.maxRange = maxRange;

        newShotScript.standardDamage = standardDamage;

        newShotScript.fireResult = fireObject;
        newShotScript.shotLifetime = shotLifetime;
        newShotScript.shotVelocity = shotVelocity;
        newShotScript.fireScript = fireScript;

        newShotScript.barrageDelay = barrageDelay;
        newShotScript.barrageSize = barrageSize;
        newShotScript.barrageCount = barrageCount;

        newShotScript.parentWeapon = this.gameObject;
        newShotScript.shotTarget = targetPosition;

        newShotScript.enabled = true;

    }
}
