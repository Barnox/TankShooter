using UnityEngine;

[CreateAssetMenu(fileName = "NewTankyWeapon", menuName = "Tanky/TankyWeapon")]
public class TankyWeapon : TankyParts
{
    public float weaponAccuracy;
    public float weaponRateoffire;
    public Vector3 firingOffset;

    public GameObject fireResult;
    public WeaponShotScript fireScript;
    public float maxRange;

    public float standardDamage;

    public float shotLifetime;
    public float shotVelocity;

    public float barrageDelay;
    public int barrageSize;
    public int barrageCount;

    public GameObject firingObject;

    public TankyWeapon()
    {
        partType = partTypes.Weapon;
    }
}
