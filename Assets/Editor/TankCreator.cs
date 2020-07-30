using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;

public class TankCreator : OdinEditorWindow
{

    GameObject newTank;
    GameObject newPart;
    GameObject newWeaponPart;
    GameObject newMeshChild;
    GameObject newBasePart;
    GameObject newMiscPart;
    GameObject newMobilityPart;

    TankyHealth partHealth;

    NavMeshAgent ownNavMeshAgent;
    TankCoreFunctions newTankCoreFunctions;

    [MenuItem("TankTools/TankCreator")]
    private static void OpenWindow()
    {
        GetWindow<TankCreator>().Show();
    }


    [AssetSelector(Paths = "Assets/Tanks/Tank Parts/Body")]
    public TankyBody TankBodySelector;

    [AssetSelector(Paths = "Assets/Tanks/Tank Parts/Mobility")]
    public TankyMobility TankMobilitySelector;

    [AssetSelector(Paths = "Assets/Tanks/Tank Parts/Weapon|Assets/Tanks/Tank Parts/Misc")]
    public TankyParts[] TankAttachmentSelector;

    public Vector2 InitialPosition;


    [Button(ButtonSizes.Medium)]
    private void ReCalculateDropdowns()
    {
        int NoOfAttachments = TankBodySelector.connectorAngleOffset.Length;
        TankAttachmentSelector = new TankyParts[NoOfAttachments];
    }

    [Button(ButtonSizes.Medium)]
    private void TankCreateButton()
    {
        Vector3 WorldPosition = new Vector3 (InitialPosition.x, 0f, InitialPosition.y);

        CreateTank(WorldPosition,TankBodySelector,TankMobilitySelector,TankAttachmentSelector);
    }

    [Button(ButtonSizes.Medium)]
    private void ClearWindow()
    {
        TankBodySelector = null;
        TankMobilitySelector = null;
        TankAttachmentSelector = new TankyParts[0];
        InitialPosition = Vector2.zero;
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
                GenerateWeapon(partToSpawn as TankyWeapon);
                break;

            case (partTypes.Body):
                GenerateBody(partToSpawn as TankyBody);
                break;

            case (partTypes.Mobility):
                GenerateMobility(partToSpawn as TankyMobility);
                break;

            case (partTypes.Misc):
                GenerateMisc(partToSpawn as TankyMisc);
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
