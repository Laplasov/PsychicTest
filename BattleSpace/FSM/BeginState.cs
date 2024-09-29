using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BeginState : BaseBattleState
{
    private bool _isSetupComplete = false;
    public override void EnteringState(BattleLogic logic)
    {
        logic.StartCoroutine(SetupBattle(logic));
    }
    public override void UpdateState(BattleLogic logic)
    {
        if (_isSetupComplete)
            logic.SwitchState(logic.PlayerState);
        
    }
    IEnumerator SetupBattle(BattleLogic logic)
    {
        logic.Massage.SetText("Battle begins!!!");

        int i = 0;
        foreach (EnemyUnitScriptableObject so in InitBattleData.EnemyUnitsInitData)
        {
            GameObject enemyUnitPrefab = so.EnemyUnitPrefab;
            GameObject enemyUnit = GameObject.Instantiate(enemyUnitPrefab, logic.EnemySpown[i].transform.position, logic.EnemySpown[i].transform.rotation);
            enemyUnit.transform.SetParent(logic.EnemySpown[i].transform);
            EnemyUnit enemyUnitScript = enemyUnit.GetComponent<EnemyUnit>();
            logic.EnemyUnits.Add(enemyUnitScript); 
            enemyUnitScript.SO = so; 
            i++;
        }
        yield return new WaitForSeconds(2f);
        _isSetupComplete = true;
    }

}
