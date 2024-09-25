using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        yield return new WaitForSeconds(2f);
        _isSetupComplete = true;
    }

}
