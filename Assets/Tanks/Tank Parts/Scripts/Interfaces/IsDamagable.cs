using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a basic interface with a single required
//method.
public interface ITakesDamage
{
    void Damage(float damageTaken);
}

public interface IPartBreakable
{

    void PartBreak();
}

