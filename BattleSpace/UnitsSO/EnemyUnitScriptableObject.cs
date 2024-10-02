using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyUnitScriptableObject", menuName = "EnemyUnitScriptableObject/Shadow")]
public class EnemyUnitScriptableObject : ScriptableObject
{
    [SerializeField]
    private string UnitName = "Shadow";
    [SerializeField]
    private string Loyalty = "Foe";
    [SerializeField]
    private bool IsElite = false;
    [SerializeField]
    public GameObject EnemyUnitPrefab;
    [Header("Stats")]
    [SerializeField]
    private int minUnitLevel = 1;
    [SerializeField]
    private int maxUnitLevel = 3;
    [SerializeField]
    private int maxHealth = 25;
    [SerializeField]
    private int minHealth = 35;
    [SerializeField]
    private int minUnitAttack = 3;
    [SerializeField]
    private int maxUnitAttack = 8;
    [SerializeField]
    private int minUnitDefence = 1;
    [SerializeField]
    private int maxUnitDefence = 5;
    [SerializeField]
    private int minUnitSkillPoints = 1;
    [SerializeField]
    private int maxUnitSkillPoints = 5;
    [Header("Multiplication")]
    [SerializeField]
    private int DefenceLvlMultiplication = 0;
    [SerializeField]
    private int SkillPointsLvlMultiplication = 0;
    [SerializeField]
    private int AttackLvlMultiplication = 0;
    [SerializeField]
    private int HealthLvlMultiplication = 0;

    [HideInInspector]
    public int UnitLevel;
    [HideInInspector]
    public int UnitAttack;
    [HideInInspector]
    public int UnitDefence;
    [HideInInspector]
    public int UnitSkillPoints;
    [HideInInspector]
    public int UnitCurrentHealth;
    public EnemyUnitStats GenerateRandomStats()
    {
        string unitName = UnitName;
        string loyalty = Loyalty;
        bool elite = IsElite;
        int unitLevel = Random.Range(minUnitLevel, maxUnitLevel);
        int unitAttack = Random.Range(minUnitAttack, maxUnitAttack) + (unitLevel * AttackLvlMultiplication);
        int unitDefence = Random.Range(minUnitDefence, maxUnitDefence) + (unitLevel * DefenceLvlMultiplication);
        int unitSkillPoints = Random.Range(minUnitSkillPoints, maxUnitSkillPoints) + (unitLevel * SkillPointsLvlMultiplication);
        int unitCurrentHealth = Random.Range(minHealth, maxHealth) + (unitLevel * HealthLvlMultiplication);
        int unitMaxHealth = unitCurrentHealth;

        return new EnemyUnitStats(unitName, loyalty, elite, unitLevel, unitAttack, unitDefence, unitSkillPoints, unitCurrentHealth, unitMaxHealth, EnemyUnitPrefab);

    }

}
[System.Serializable]
public class EnemyUnitStats
{
    public string UnitName;
    public string Loyalty;
    public bool IsElite;
    public int UnitLevel;
    public int UnitAttack;
    public int UnitDefence;
    public int UnitSkillPoints;
    public int UnitCurrentHealth;
    public int UnitMaxHealth;
    public GameObject EnemyUnitPrefab;


    public EnemyUnitStats(string unitName, string loyalty, bool elite, int unitLevel, int unitAttack, int unitDefence, int unitSkillPoints, int unitCurrentHealth, int unitMaxHealth, GameObject enemyUnitPrefab)
    {
        UnitName = unitName;
        Loyalty = loyalty;
        IsElite = elite;
        UnitLevel = unitLevel;
        UnitAttack = unitAttack;
        UnitDefence = unitDefence;
        UnitSkillPoints = unitSkillPoints;
        UnitCurrentHealth = unitCurrentHealth;
        UnitMaxHealth = unitMaxHealth;
        EnemyUnitPrefab = enemyUnitPrefab;
    }
}