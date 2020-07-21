using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankyHealth : MonoBehaviour, ITakesDamage, IPartBreakable
{
    // Start is called before the first frame update

    public float maxHealth;
    public float currentHealth;
    TankyHealth[] childrenHealth;

    public float cashValue;
    public TankyParts ownPart;

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            PartBreak();
        }
    }

    public void PartBreak()
    {

        //Find all children with health. Break them too.
        childrenHealth = GetComponentsInChildren<TankyHealth>();

            foreach (TankyHealth childHealth in childrenHealth)
            {
                if (childHealth.gameObject != gameObject)
                {
                    childHealth.PartBreak();

                }
            }

        //Spawn some loot

        //Tell the tank root to delete this object and refactor.
        transform.root.gameObject.GetComponent<TankCoreFunctions>().DestroyPart(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
