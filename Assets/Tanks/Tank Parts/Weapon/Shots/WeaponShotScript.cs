using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotScript : MonoBehaviour
{
    public float weaponAccuracy;
    public float weaponRateoffire;
    public Vector3 firingOffset;

    public float maxRange;

    public float standardDamage;

    public GameObject fireResult;
    public float shotLifetime;
    public float shotVelocity;
    public WeaponShotScript fireScript;

    public float barrageDelay;
    public int barrageSize;
    public int barrageCount;

    public Vector3 shotTarget;

}
