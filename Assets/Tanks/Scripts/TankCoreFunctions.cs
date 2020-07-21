using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankCoreFunctions : MonoBehaviour
{

    public GameObject bodyPart;
    public GameObject mobilityPart;
    public GameObject[] weaponParts;
    public GameObject[] miscParts;
    NavMeshAgent ownNavMeshAgent;
    AIAiming ownAiming;
    AIMoving ownMoving;

    TankyHealth[] allParts;

    public float accelSpeed;
    public float maxSpeed;
    public float turnSpeed;
    public float turretRotateSpeed = 90;

    int numberOfWeapons;

    // Start is called before the first frame update
    void Start()
    {
        ownNavMeshAgent = GetComponent<NavMeshAgent>();
        ownAiming = GetComponent<AIAiming>();
        ownMoving = GetComponent<AIMoving>();
        Refactor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refactor()
    {

        allParts = null;
        weaponParts = null;


        allParts = GetComponentsInChildren<TankyHealth>();

        numberOfWeapons = 0;
        
        foreach (TankyHealth indivPart in allParts )
        {
            if (indivPart.ownPart.partType == partTypes.Weapon)
            { numberOfWeapons++; }
        }

        weaponParts = new GameObject[numberOfWeapons];
        numberOfWeapons = 0;

        foreach (TankyHealth indivPart in allParts)
        {

            switch (indivPart.ownPart.partType)
            {
                case (partTypes.Weapon):
                    TankyWeapon weaponPart = (TankyWeapon)indivPart.ownPart;
                    weaponParts[numberOfWeapons] = indivPart.gameObject;
                    numberOfWeapons++;
                    break;

                case (partTypes.Body):

                    break;

                case (partTypes.Mobility):
                    TankyMobility mobilityPart = (TankyMobility)indivPart.ownPart;
                    accelSpeed = mobilityPart.accelSpeed;
                    maxSpeed = mobilityPart.maxSpeed;
                    turnSpeed = mobilityPart.turnSpeed;

                    ownMoving.turnSpeed = turnSpeed;
                    ownMoving.moveAccel = accelSpeed;
                    ownMoving.maxSpeed = maxSpeed;
                    ownMoving.maxReverse = maxSpeed * -1;

                    ownNavMeshAgent.speed = 0;
                    ownNavMeshAgent.angularSpeed = turnSpeed;
                    ownNavMeshAgent.radius = 1;
                    break;

            }


        }
    }

    public void DestroyPart(GameObject partToDestroy)
    {
        Debug.Log("DestroyPart called");
        if (partToDestroy.gameObject == bodyPart.gameObject)
        { FullyDestroyTank();
            Debug.Log("True");}
        else
        {
            Debug.Log("False");
            GameObject.Destroy(partToDestroy);
            Refactor();
        }
    }

    public void FullyDestroyTank()
    { GameObject.Destroy(gameObject); }

}
