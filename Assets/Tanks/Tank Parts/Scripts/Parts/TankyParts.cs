using UnityEngine;


public enum partTypes { Weapon, Body, Mobility, Misc };

public class TankyParts : ScriptableObject
{

    public string partName;
    public Material partTexture;
    public Mesh partMesh;

    public Vector3 ownConnectorDistanceOffset;
    public Vector3 ownConnectorAngleOffset;

    public float partIndividualHealth;

    public partTypes partType;


}
