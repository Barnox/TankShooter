using UnityEngine;


[CreateAssetMenu(fileName = "NewTankyMobility", menuName = "Tanky/TankyMobility")]
public class TankyMobility : TankyParts
{
    public float maxSpeed;
    public float accelSpeed;
    public float turnSpeed;
    public bool canStrafe;

    public TankyMobility()
    {
        partType = partTypes.Mobility;
    }
}
