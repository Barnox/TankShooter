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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.transform.name);
        if (other.transform.parent != null)
        {
            if (other.gameObject.transform.parent.GetComponent<ITakesDamage>() != null)
            {
                other.gameObject.transform.parent.GetComponent<ITakesDamage>().Damage(standardDamage); 
            }
        }

        Destroy(gameObject);

    }
}
