using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleData : MonoBehaviour
{
    //public static List<EnemyUnitScriptableObject> EnemyUnitsInitData = new List<EnemyUnitScriptableObject>();
    public static List<EnemyUnitStats> EnemyUnitsInitData = new List<EnemyUnitStats>();
    public static List<AttackItemsSO> AttackItemsSO = new List<AttackItemsSO>();
    public static List<DefenceItemsSO> DefenceItemsSO = new List<DefenceItemsSO>();
    public static List<SkillsSO> SkillsSO = new List<SkillsSO>();
    public static GameObject Player;
    public static bool BattleScene = false;
}
