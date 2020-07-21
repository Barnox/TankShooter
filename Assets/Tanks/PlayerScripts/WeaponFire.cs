using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    Vector3 fireOffset;
    GameObject fireObject;
    float refireRate;
    float sinceLastShot;
    public float maxRange;
    TankyWeapon thisWeapon;
    Vector3 offsetPosition;


    // Start is called before the first frame update
    void Start()
    {
        thisWeapon = (TankyWeapon)GetComponent<TankyHealth>().ownPart;
        fireOffset = thisWeapon.firingOffset;
        refireRate = thisWeapon.weaponRateoffire;
        fireObject = thisWeapon.fireResult;
        maxRange = thisWeapon.maxRange;

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
            offsetPosition = gameObject.transform.TransformPoint(fireOffset);
            Instantiate(fireObject, offsetPosition, gameObject.transform.rotation);
            sinceLastShot = 0;
        }

    }
}
