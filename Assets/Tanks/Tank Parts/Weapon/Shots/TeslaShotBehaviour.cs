using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaShotBehaviour : WeaponShotScript
{

    public int numberOfPoints = 5;
    float pointDistance;
    Vector3[] pointPosition;

    float rayDistance;
    Ray damageRay;
    RaycastHit castHit;

    

    // Start is called before the first frame update
    void Start()
    {
        FireLightning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireLightning()
    {
        rayDistance = maxRange;
        damageRay = new Ray(transform.position, gameObject.transform.forward * maxRange);
        Debug.DrawRay(transform.position, transform.forward * maxRange, Color.magenta, 5);
        if (Physics.Raycast(damageRay, out castHit))
        {
            if (castHit.rigidbody != null)
            {
                Debug.Log(castHit.rigidbody);

                if (castHit.rigidbody.transform.parent.GetComponent<ITakesDamage>() != null) { castHit.rigidbody.transform.parent.GetComponent<ITakesDamage>().Damage(standardDamage); }
                rayDistance = castHit.distance;
            }
        }

        pointPosition = new Vector3[numberOfPoints];
        pointDistance = rayDistance / numberOfPoints;


        for (int i = 0; i < numberOfPoints; i++)
        {
            pointPosition[i] = transform.position + (transform.forward * pointDistance * (i + 1)) + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        }

        Debug.DrawLine(transform.position, pointPosition[0], Color.blue,5);
        for (int i = 0; i < numberOfPoints - 1; i++)
        {
            Debug.DrawLine(pointPosition[i], pointPosition[i + 1], Color.blue, 5);
            gameObject.transform.position = pointPosition[i];
            //pointPosition[i] = transform.InverseTransformVector(pointPosition[i]);
        }

        gameObject.transform.position = pointPosition[numberOfPoints - 1];

        Destroy(gameObject, 2);
    }
}
