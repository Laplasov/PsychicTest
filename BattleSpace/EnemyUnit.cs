using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyUnit : MonoBehaviour
{
    EnemyUnitScriptableObject _so;
    public EnemyUnitStats stats;

    public string UnitName;
    public string Loyalty;
    public int UnitLevel;
    public int UnitAttack;
    public int UnitDefence;
    public int UnitSkillPoints;
    public int UnitMaxHealth;
    public int UnitCurrentHealth;

    public EnemyUnitScriptableObject SO 
    {
        get { return _so; } 
        set { _so = value; }
    }

    void Start()
    {
        stats = _so.GenerateRandomStats();
        UnitName = stats.UnitName;
        Loyalty = stats.Loyalty;
        UnitLevel = stats.UnitLevel;
        UnitAttack = stats.UnitAttack;
        UnitDefence = stats.UnitDefence;
        UnitSkillPoints = stats.UnitSkillPoints;
        UnitCurrentHealth = stats.UnitCurrentHealth;
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
    void OnMouseEnter()
    {
        HUD.onObjectHovered.Invoke(this);
    }
    void OnMouseExit()
    {
        HUD.onObjectExit.Invoke();
    }
    void OnMouseDown()
    {
        HUD.onObjectDown.Invoke(this);
    }
}

