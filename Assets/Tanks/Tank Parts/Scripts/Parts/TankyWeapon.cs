using UnityEngine;

[CreateAssetMenu(fileName = "NewTankyWeapon", menuName = "Tanky/TankyWeapon")]
public class TankyWeapon : TankyParts
{
    public float weaponAccuracy;
    public float weaponRateoffire;
    public Vector3 firingOffset;
    public GameObject fireResult;
    public float maxRange;


    public TankyWeapon()
    {
        partType = partTypes.Weapon;
    }
}
