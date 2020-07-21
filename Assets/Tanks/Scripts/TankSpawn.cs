using System;
using UnityEngine;
using UnityEngine.AI;

public class TankSpawn : MonoBehaviour
{
    public PartCollection thisCollection;

    public TankyBody basePart;
    public TankyWeapon weaponPart;
    public TankyMobility mobilityPart;
    public TankyMisc miscPart;
    GameObject newTank;
    GameObject newBasePart;
    GameObject newMobilityPart;
    GameObject newPart;
    GameObject newMeshChild;
    GameObject newMiscPart;
    GameObject newWeaponPart;
    TankCoreFunctions newTankCoreFunctions;
    NavMeshAgent ownNavMeshAgent;

    TankyHealth partHealth;


    RaycastHit hit;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CreateTank(Vector3 spawnPosition, TankyBody basePart, TankyMobility mobilityPart, TankyParts[] attachParts)
    {
        //basePart = thisCollection.collectedBody[UnityEngine.Random.Range(0, thisCollection.collectedBody.Length)];
        //mobilityPart = thisCollection.collectedMobility[UnityEngine.Random.Range(0, thisCollection.collectedMobility.Length)];

        newTank = new GameObject("SpawnedTank");
        newTank.transform.position = spawnPosition;
        ownNavMeshAgent = newTank.AddComponent<NavMeshAgent>();
        ownNavMeshAgent.speed = 0;
        newTank.AddComponent<AIAiming>();
        newTank.AddComponent<AIMoving>();
        newTankCoreFunctions = newTank.AddComponent<TankCoreFunctions>();

        GeneratePart(basePart, newTank.transform);
        newTankCoreFunctions.bodyPart = newPart;
        newBasePart.transform.localPosition -= mobilityPart.ownConnectorDistanceOffset;

        //Offset Body
        GeneratePart(mobilityPart, newTank.transform);
        newTankCoreFunctions.mobilityPart = newPart;

        for (int i = 0; i < attachParts.Length; i++)
        {
            if (attachParts[i] != null)
            {
                GeneratePart(attachParts[i], newBasePart.transform, basePart.connectorDistanceOffset[i], basePart.connectorAngleOffset[i]);
            }

        }


        /*
        for (int i = 0; i < basePart.connectorAngleOffset.Length; i++)
        {
            weaponPart = thisCollection.collectedWeapon[UnityEngine.Random.Range(0, thisCollection.collectedWeapon.Length)];
            miscPart = thisCollection.collectedMisc[UnityEngine.Random.Range(0, thisCollection.collectedMisc.Length)];
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    break;
                case 1:
                    GeneratePart(weaponPart, newBasePart.transform, basePart.connectorDistanceOffset[i], basePart.connectorAngleOffset[i]);
                    break;
                case 2:
                    GeneratePart(miscPart, newBasePart.transform, basePart.connectorDistanceOffset[i], basePart.connectorAngleOffset[i]);
                    break;
            }

        }
        */

    }

    void GeneratePart(TankyParts partToSpawn, Transform Parent)
    {
        GeneratePart(partToSpawn, Parent, Vector3.zero, Vector3.zero);
    }



    void GeneratePart(TankyParts partToSpawn, Transform Parent, Vector3 offsetDistance, Vector3 offsetAngle)
    {
        //New empty GameObject
        newPart = new GameObject(partToSpawn.partName);
        ReParent(newPart.transform, Parent);

        //Offset self
        newPart.transform.localPosition = offsetDistance;
        newPart.transform.localRotation = Quaternion.Euler(offsetAngle);

        //Set up the health system for the part
        partHealth = newPart.AddComponent<TankyHealth>();
        partHealth.maxHealth = partToSpawn.partIndividualHealth;
        partHealth.ownPart = partToSpawn;

        //Create the model as a child
        newMeshChild = new GameObject(partToSpawn.partName + "Mesh");
        ReParent(newMeshChild.transform, newPart.transform);
        newMeshChild.AddComponent<MeshFilter>().mesh = partToSpawn.partMesh;
        newMeshChild.AddComponent<MeshRenderer>().material = partToSpawn.partTexture;
        newMeshChild.AddComponent<BoxCollider>();
        newMeshChild.AddComponent<Rigidbody>().isKinematic = true;

        //Add model at offset

        switch (partToSpawn.partType)
        {
            case (partTypes.Weapon):
                GenerateWeapon(weaponPart);
                break;

            case (partTypes.Body):
                GenerateBody(basePart);
                break;

            case (partTypes.Mobility):
                GenerateMobility(mobilityPart);
                break;

            case (partTypes.Misc):
                GenerateMisc(miscPart);
                break;

        }

    }

    void GenerateWeapon(TankyWeapon partToSpawn)
    {
        newWeaponPart = newPart;
        newWeaponPart.AddComponent<WeaponFire>();
    }

    void GenerateBody(TankyBody partToSpawn)
    {
        newBasePart = newPart;
    }

    void GenerateMisc(TankyMisc partToSpawn)
    {
        newMiscPart = newPart;
    }

    void GenerateMobility(TankyMobility partToSpawn)
    {
        newMobilityPart = newPart;
    }


    void ReParent(Transform Child, Transform Parent)
    {
        Child.parent = Parent;
        Child.transform.position = Parent.transform.position;
        Child.transform.rotation = Parent.transform.rotation;
    }

}
