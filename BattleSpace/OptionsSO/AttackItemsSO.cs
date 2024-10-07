using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionObject/Weapon")]
public class AttackItemsSO : ActionFactorySO
{
    [SerializeField]
    private string _actionName;
    [SerializeField]
    private int _actionDamage;
    [SerializeField]
    private bool _actionDestructible;

    public override string Name { get { return _actionName; } }
    public override int Damage { get { return _actionDamage; } }
    public override bool Destructible { get { return _actionDestructible; } }
    public override ActionFactorySO ReturnOS() => this;
}
