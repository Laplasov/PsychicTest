using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseBattleState
{
    public override void EnteringState(BattleLogic logic)
    {
        logic.Massage.SetText("EnteringState");

        foreach (EnemyUnit enemyUnit in logic.EnemyUnits)
        {
            logic.StartCoroutine(EnemyUnitAttack(logic));
        }
    }
    public override void UpdateState(BattleLogic logic)
    {

    }
    IEnumerator EnemyUnitAttack(BattleLogic logic)
    {

        logic.Massage.SetText("Battle begins!!!");
        yield return new WaitForSeconds(2f);

    }
}
