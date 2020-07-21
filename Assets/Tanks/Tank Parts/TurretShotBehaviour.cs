using UnityEngine;

public class TurretShotBehaviour : MonoBehaviour
{

    public float velocity = 5;
    public float lifetime = 1;
    Rigidbody ownRigidbody;
    public float ownDamage = 1;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(this.gameObject, lifetime);
        ownRigidbody = GetComponent<Rigidbody>();
        ownRigidbody.velocity = transform.forward * velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.GetComponent<ITakesDamage>() != null)
            { 
                collision.gameObject.transform.parent.GetComponent<ITakesDamage>().Damage(ownDamage); 
            }
        }

        Destroy(gameObject);

    }
}
