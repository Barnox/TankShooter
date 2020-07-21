using UnityEngine;

[CreateAssetMenu(fileName = "NewTankyBody", menuName = "Tanky/TankyBody")]
public class TankyBody : TankyParts
{
    public Vector3[] connectorDistanceOffset;
        public Vector3[] connectorAngleOffset;

    public TankyBody()
    {
        partType = partTypes.Body;
    }

}
