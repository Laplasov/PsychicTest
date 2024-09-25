using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerUnitScriptableObject/Player")]
public class PlayerUnitScriptableObject : ScriptableObject
{
    [SerializeField]
    public string UnitName = "Player";

    [Header("Stats")]
    [SerializeField]
    public int UnitLevel = 1;
    [SerializeField]
    public int UnitHealth = 50;
    [SerializeField]
    public int UnitAttack = 15;
    [SerializeField]
    public int UnitDefence = 5;
    [SerializeField]
    public int UnitSkillPoints = 5;
    [SerializeField]
    public int UnitMaxHealth = 50;

    public PlayerUnitStats GetPlayerStats()
    {
        string unitName = UnitName;
        int unitLevel = UnitLevel;
        int unitAttack = UnitAttack;
        int unitDefence = UnitDefence;
        int unitSkillPoints = UnitSkillPoints;
        int unitCurrentHealth = UnitHealth;
        int unitMaxHealth = UnitMaxHealth;

        return new PlayerUnitStats(unitName,  unitLevel, unitAttack, unitDefence, unitSkillPoints, unitCurrentHealth, unitMaxHealth);
    }
}
[System.Serializable]
public class PlayerUnitStats
{
    public string UnitName;
    public int UnitLevel;
    public int UnitAttack;
    public int UnitDefence;
    public int UnitSkillPoints;
    public int UnitHealth;
    public int UnitMaxHealth;

    public PlayerUnitStats(string unitName, int unitLevel, int unitAttack, int unitDefence, int unitSkillPoints, int unitHealth, int unitMaxHealth)
    {
        UnitName = unitName;
        UnitHealth = unitHealth;
        UnitLevel = unitLevel;
        UnitAttack = unitAttack;
        UnitDefence = unitDefence;
        UnitSkillPoints = unitSkillPoints;
        UnitMaxHealth = unitMaxHealth;
    }
}