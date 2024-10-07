using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionFactorySO : ScriptableObject
{
    public abstract string Name { get; }
    public abstract int Damage { get; }
    public abstract bool Destructible { get; }
    public abstract ActionFactorySO ReturnOS();
}
