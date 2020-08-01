using UnityEngine;

public class TurretShotBehaviour : WeaponShotScript
{

    Rigidbody ownRigidbody;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Start()
    {
        Destroy(this.gameObject, shotLifetime);
        ownRigidbody = GetComponent<Rigidbody>();
        ownRigidbody.velocity = transform.forward * shotVelocity;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided with " + collision.transform.name);
        if (collision.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.GetComponent<ITakesDamage>() != null)
            { 
                collision.gameObject.transform.parent.GetComponent<ITakesDamage>().Damage(standardDamage); 
            }
        }

        Destroy(gameObject);

    }
}
