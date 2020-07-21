using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PartCollection", menuName = "Tanky/PartCollection")]

public class PartCollection: SerializedScriptableObject
{
    public TankyBody[] collectedBody;
    public TankyMisc[] collectedMisc;
    public TankyMobility[] collectedMobility;
    public TankyWeapon[] collectedWeapon;

}
