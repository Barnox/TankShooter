using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoving : MonoBehaviour
{

    public GameObject playerGO;
    TankCoreFunctions tankCore;
    NavMeshAgent ownNavMeshAgent;

    Vector3 currentVector;
    Vector3 targetVector;
    Vector3 nextCorner;
    Vector3 toNextCorner;
    Vector3 toPlayer;
    public float turnSpeed;
    public float moveAccel;
    public float targetSpeed;
    public float maxReverse;
    public float maxSpeed;
    float averageMaxRange;
    float sumMaxRange;
    LayerMask maskPlayerTerrain;

    RaycastHit castHit;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        ownNavMeshAgent = GetComponent<NavMeshAgent>();
        tankCore = GetComponent<TankCoreFunctions>();
        maskPlayerTerrain = LayerMask.GetMask("Terrain","Player");

        Invoke("CheckRange", 1);

}

    // Update is called once per frame
    void Update()
    {
        toPlayer = playerGO.transform.position - transform.position;

        nextCorner = ownNavMeshAgent.steeringTarget;
        toNextCorner = nextCorner - transform.position;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(toNextCorner, Vector3.up), turnSpeed * Time.deltaTime);


        targetSpeed += moveAccel * Vector3.Dot(transform.forward, toNextCorner) * Time.deltaTime;

        targetSpeed = Mathf.Clamp(targetSpeed, maxReverse, maxSpeed);

        currentVector = Vector3.RotateTowards(currentVector, targetVector, turnSpeed * Time.deltaTime, moveAccel * Time.deltaTime);  //Move our current vector towards our target vector. Change from where we're going to where we want to go.}

        targetVector = (transform.forward * targetSpeed); //The way we want to go is in front of us, as fast as we want to go.

        ownNavMeshAgent.Move(currentVector * Time.deltaTime);


        if (Physics.Raycast(transform.position, toPlayer, out RaycastHit castHit, Mathf.Infinity, maskPlayerTerrain))
        {
            if (castHit.collider.tag != "Player") {
                
                    ownNavMeshAgent.SetDestination(playerGO.transform.position);
                }
                else
                {
                if (toPlayer.magnitude >= averageMaxRange) { ownNavMeshAgent.SetDestination(playerGO.transform.position); }
                }
            }

        }

    void CheckRange()
    {
        int numOfWeapons = 0;
        foreach (GameObject indivWeapon in tankCore.weaponParts)
        {
            if (indivWeapon != null) { sumMaxRange += indivWeapon.GetComponent<WeaponFire>().maxRange; numOfWeapons++; }
        }
        averageMaxRange = (sumMaxRange / numOfWeapons) * 0.8f;

    }

    }
