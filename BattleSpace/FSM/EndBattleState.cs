using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleState : BaseBattleState
{
    private bool _isEndStateEnded = false;
    public override void EnteringState(BattleLogic logic)
    {
        _isEndStateEnded = false;
        if (logic.PlayerUnit.UnitCurrentHealth <= 0)
            logic.Massage.SetText("You have lost!");
        else if (logic.EnemyUnits.Count == 0)
            logic.Massage.SetText("You have won!");
        else
            logic.Massage.SetText("It is wierd!");
        logic.StartCoroutine(EndBattle());

    }
    public override void UpdateState(BattleLogic logic)
    {
        if (_isEndStateEnded)
            SceneButtonProxy.OnButtonClick();

    }
    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(2f);
        _isEndStateEnded = true;
    }
}
