using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : BaseBattleState
{
    public static bool WaitingForAction = false;
    public static bool ActionEnded = false;
    public static bool IsPlayerState = false;

    public override void EnteringState(BattleLogic logic)
    {
        IsPlayerState = true;
        string targetList = "Choose your target:\n";

        foreach (EnemyUnit enemyUnit in logic.EnemyUnits)
        {
            targetList += $"{enemyUnit.UnitName} - Lvl {enemyUnit.UnitLevel}" + "\n";
        }
        logic.Massage.SetText(targetList);
        WaitingForAction = true;

    }
    public override void UpdateState(BattleLogic logic)
    {
        if (WaitingForAction && ActionEnded) 
        {
            logic.StartCoroutine(ActionTime(logic));
            logic.SwitchState(logic.EnemyState);
        }
        
    }
    IEnumerator ActionTime(BattleLogic logic)
    {
        logic.Massage.SetText("Attack!!!");
        WaitingForAction = false;
        ActionEnded = false;
        IsPlayerState = false;
        yield return new WaitForSeconds(2f);
        if (logic.EnemyUnits.Count == 0)
            logic.SwitchState(logic.EndBattleState);
        else
            logic.SwitchState(logic.EnemyState);
    }

}
