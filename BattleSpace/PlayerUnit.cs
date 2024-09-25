using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public string UnitName;
    public string Loyalty;
    public int UnitLevel;
    public int UnitAttack;
    public int UnitDefence;
    public int UnitSkillPoints;
    public int UnitCurrentHealth;
    public int UnitMaxHealth;
    [SerializeField]
    PlayerUnitScriptableObject _so;
    public PlayerUnitStats stats;
    public List<AttackItemsSO> AttackOptions;
    public List<AttackItemsSO> DefenceOptions;
    public List<AttackItemsSO> SkillOptions;

    void Awake()
    {
        stats = _so.GetPlayerStats();
        UnitName = stats.UnitName;
        UnitLevel = stats.UnitLevel;
        UnitAttack = stats.UnitAttack;
        UnitDefence = stats.UnitDefence;
        UnitSkillPoints = stats.UnitSkillPoints;
        UnitCurrentHealth = stats.UnitHealth;
        UnitMaxHealth = stats.UnitMaxHealth;
    }

    public void TakeDamage(int Damage)
    {
        if (Damage > UnitDefence)
        {
            Damage -= UnitDefence;
            UnitCurrentHealth -= Damage;
        }
    }

}
