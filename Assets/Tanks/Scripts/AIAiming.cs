using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAiming : MonoBehaviour
{

    public GameObject playerGO;
    Vector3 toPlayer;
     float angleBetween;
    public float maxAngle = 3;
    GameObject bodyPart;
    TankCoreFunctions tankCore;
    WeaponFire indivFire;
    LayerMask maskPlayerTerrain;

    RaycastHit castHit;


    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        bodyPart = GetComponent<TankCoreFunctions>().bodyPart;

        tankCore = GetComponent<TankCoreFunctions>();
        maskPlayerTerrain = LayerMask.GetMask("Terrain", "Player");
    }

    // Update is called once per frame
    void Update()
    {
        toPlayer = playerGO.transform.position - bodyPart.transform.position;
        toPlayer.y = transform.position.y;
        angleBetween = Vector3.Angle(bodyPart.transform.forward, toPlayer);

        if (Physics.Raycast(transform.position, toPlayer, out RaycastHit castHit, Mathf.Infinity, maskPlayerTerrain))
        {
            if (castHit.collider.tag == "Player")
            {

                if (angleBetween <= maxAngle)
                {
                    //for each weapon, if toPlayer.Magnitude < weaponrange, weapon.Fire()

                    foreach (GameObject indivWeapon in tankCore.weaponParts)
                    {
                        if (indivWeapon != null)
                        {
                            indivFire = indivWeapon.GetComponent<WeaponFire>();
                            if (indivFire.maxRange >= toPlayer.magnitude)
                            {
                                indivFire.Fire(playerGO.transform.position);
                            }
                        }
                    }
                }
                else
                {
                    //bodyPart.transform.rotation = Quaternion.Euler( Vector3.RotateTowards(bodyPart.transform.forward, toPlayer, tankCore.turretRotateSpeed,0));
                    //bodyPart.transform.rotation.SetLookRotation(Vector3.RotateTowards(bodyPart.transform.rotation.eulerAngles, toPlayer, tankCore.turretRotateSpeed * Time.deltaTime, 0), bodyPart.transform.up);
                    //bodyPart.transform.rotation = Quaternion.Euler(new Vector3(bodyPart.transform.rotation.eulerAngles.x, bodyPart.transform.rotation.eulerAngles.y + 1, bodyPart.transform.rotation.eulerAngles.z));
                    bodyPart.transform.rotation = Quaternion.RotateTowards(bodyPart.transform.rotation, Quaternion.LookRotation(toPlayer, Vector3.up), tankCore.turretRotateSpeed * Time.deltaTime);
                }
            }
        }    
    }
}
