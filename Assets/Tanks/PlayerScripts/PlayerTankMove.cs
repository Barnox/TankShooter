using UnityEngine;
using UnityEngine.AI;

public class PlayerTankMove : MonoBehaviour
{
    public float maxSpeed = 5;
    public float maxReverse = -5;
    public float targetChangeSpeed = 3;
    public float moveAccel = 5f;
    public float turnSpeed = 30;
    public float turretSpeed = 60;
    public bool canStrafe = false;
    public float turnSpeedConvert = 0.5f;
    float targetSpeed;
    public Vector3 targetVector;
    public Vector3 currentVector;

    //Rigidbody ownRigidbody;
    NavMeshAgent ownNavMeshAgent;


    public GameObject basePart;





    private void Awake()
    {
        //ownRigidbody = GetComponent<Rigidbody>();
        ownNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    //For Rigidbody Movement
    /*
    private void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0); //Turn left and right

        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition); //Do a ray from the camera to the mouse
        Plane playerPlane = new Plane(Vector3.up, basePart.transform.position); //Make a plane at the player's height
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera); //Get where they collide
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera); //Take the point where they collide
        Vector3 vectorToCursor = cursorPosition - basePart.transform.position; //Make a vector going from the basePart to that point

        basePart.transform.LookAt(cursorPosition); //Test to get the turret looking the right way

        //        Quaternion toRotation = Quaternion.FromToRotation(basePart.transform.forward, vectorToCursor); //Work out what rotation goes between the two vectors
        //        basePart.transform.rotation = Quaternion.Lerp(basePart.transform.rotation, toRotation, turretSpeed * Time.deltaTime); //Lerp between current rotation and that new rotation at turretSpeed rate






        targetSpeed += Input.GetAxis("Vertical") * Time.deltaTime * targetChangeSpeed; //Move the target 

        if (Input.GetButtonDown("Jump")) { targetSpeed = 0.001f; } //Brake.

        //if (targetSpeed != 0) { currentVector = Quaternion.Euler(0, Input.GetAxis("Horizontal") * turnSpeedConvert, 0) * currentVector; } //Why did I have this on?


        targetSpeed = Mathf.Clamp(targetSpeed, maxReverse, maxSpeed); //Clamp target Speed to our top speed.
        targetVector = (transform.forward * targetSpeed); //The way we want to go is in front of us, as fast as we want to go.
                                                          
        //currentVector = Vector3.RotateTowards(currentVector, targetVector, turnSpeedConvert * Time.deltaTime, moveAccel * Time.deltaTime); //Move our current vector towards our target vector. Change from where we're going to where we want to go.
        
        currentVector = Vector3.RotateTowards(currentVector, targetVector, turnSpeed * Time.deltaTime, moveAccel * Time.deltaTime);  //Move our current vector towards our target vector. Change from where we're going to where we want to go.}


        ownRigidbody.velocity = currentVector; //The actual movement
    }
    */


    //For non-rigidbody movement
    
     private void Update()
     {
         transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0); //Turn left and right

         Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition); //Do a ray from the camera to the mouse
         Plane playerPlane = new Plane(Vector3.up, basePart.transform.position); //Make a plane at the player's height
         playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera); //Get where they collide
         Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera); //Take the point where they collide
         Vector3 vectorToCursor = cursorPosition - basePart.transform.position; //Make a vector going from the basePart to that point

         basePart.transform.LookAt(cursorPosition); //Test to get the turret looking the right way

         //        Quaternion toRotation = Quaternion.FromToRotation(basePart.transform.forward, vectorToCursor); //Work out what rotation goes between the two vectors
         //        basePart.transform.rotation = Quaternion.Lerp(basePart.transform.rotation, toRotation, turretSpeed * Time.deltaTime); //Lerp between current rotation and that new rotation at turretSpeed rate






         targetSpeed += Input.GetAxis("Vertical") * Time.deltaTime * targetChangeSpeed; //Move the target 

         if (Input.GetButtonDown("Jump")) { targetSpeed = 0.001f; } //Brake.
          currentVector = Vector3.RotateTowards(currentVector, targetVector, turnSpeed * Time.deltaTime, moveAccel * Time.deltaTime);  //Move our current vector towards our target vector. Change from where we're going to where we want to go.}

         //if (targetSpeed != 0) { currentVector = Quaternion.Euler(0, Input.GetAxis("Horizontal") * turnSpeedConvert, 0) * currentVector; } //Why did I have this on?


         targetSpeed = Mathf.Clamp(targetSpeed, maxReverse, maxSpeed); //Clamp target Speed to our top speed.
         targetVector = (transform.forward * targetSpeed); //The way we want to go is in front of us, as fast as we want to go.
                                                           //currentVector = Vector3.RotateTowards(currentVector, targetVector, turnSpeedConvert * Time.deltaTime, moveAccel * Time.deltaTime); //Move our current vector towards our target vector. Change from where we're going to where we want to go.


        //transform.position += currentVector * Time.deltaTime; //The actual movement
        ownNavMeshAgent.Move(currentVector * Time.deltaTime);

     }
     


}
