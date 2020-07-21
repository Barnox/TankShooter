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
    public float turnSpeed;
    public float moveAccel;
    public float targetSpeed;
    public float maxReverse;
    public float maxSpeed;


    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        ownNavMeshAgent = GetComponent<NavMeshAgent>();
        tankCore = GetComponent<TankCoreFunctions>();

}

    // Update is called once per frame
    void Update()
    {

        nextCorner = ownNavMeshAgent.steeringTarget;
        toNextCorner = nextCorner - transform.position;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(toNextCorner, Vector3.up), turnSpeed * Time.deltaTime);


        targetSpeed  += moveAccel * Vector3.Dot(transform.forward, toNextCorner) * Time.deltaTime;

        targetSpeed = Mathf.Clamp(targetSpeed, maxReverse, maxSpeed);

        currentVector = Vector3.RotateTowards(currentVector, targetVector, turnSpeed * Time.deltaTime, moveAccel * Time.deltaTime);  //Move our current vector towards our target vector. Change from where we're going to where we want to go.}
        
        targetVector = (transform.forward * targetSpeed); //The way we want to go is in front of us, as fast as we want to go.

        ownNavMeshAgent.Move(currentVector * Time.deltaTime);



        ownNavMeshAgent.SetDestination(playerGO.transform.position);
    }
}
