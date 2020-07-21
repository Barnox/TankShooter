using UnityEngine;

public class PlayerFireTest : MonoBehaviour
{

    public GameObject weaponPart;
    public GameObject shotPart;
    public Vector3 weaponFireOffset;
    Vector3 offsetPosition;
    public float fireDelay = 0.5f;
    float sinceLastShot;
    GameObject newShot;
    PlayerTankMove ownMove;
    // Start is called before the first frame update
    void Awake()
    {
        ownMove = GetComponent<PlayerTankMove>();
        sinceLastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        sinceLastShot += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (sinceLastShot >= fireDelay)
            {
                offsetPosition = weaponPart.transform.TransformPoint(weaponFireOffset);
                newShot = Instantiate(shotPart, offsetPosition, weaponPart.transform.rotation);
                newShot.GetComponent<Rigidbody>().velocity += ownMove.currentVector;
                sinceLastShot = 0;
            }


        }
    }
}
