using UnityEngine;

[CreateAssetMenu(fileName = "NewTankyMisc", menuName = "Tanky/TankyMisc")]
public class TankyMisc : TankyParts
{
    public float additionalSpeed;
    public float additionalRadar;
    public float additionalAbility;

    public TankyMisc()
    {
        partType = partTypes.Misc;
    }

}
