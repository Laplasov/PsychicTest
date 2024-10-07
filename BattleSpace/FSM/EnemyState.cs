using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseBattleState
{
    private bool _isEnemyTurnComplited;
    public override void EnteringState(BattleLogic logic)
    {
        _isEnemyTurnComplited = false;
        logic.Massage.SetText("Enemy Turn");

        logic.StartCoroutine(EnemyUnitAttack(logic));
    }
    public override void UpdateState(BattleLogic logic)
    {
        if (_isEnemyTurnComplited && logic.EnemyUnits.Count > 0)
        {
            _isEnemyTurnComplited = false;
            logic.SwitchState(logic.PlayerState);
        }

    }
    IEnumerator EnemyUnitAttack(BattleLogic logic)
    {
        List<EnemyUnit> enemyUnits = new List<EnemyUnit>(logic.EnemyUnits);
        yield return new WaitForSeconds(2f);
        int i = 1;
        foreach (EnemyUnit enemyUnit in enemyUnits)
        {

            int Damage = logic.PlayerUnit.TakeDamage(enemyUnit.UnitAttack);
            HUDPlayer.onPlayerAttacked.Invoke();
            if (Damage > 0) 
                logic.Massage.SetText($"{i} - Enemy inflict {Damage} damage!");
            else 
                logic.Massage.SetText($"{i} - Enemy damage blocked!");
            if (logic.PlayerUnit.UnitCurrentHealth <= 0)
            {
                logic.SwitchState(logic.EndBattleState);
                break;
            }
            i++;
            yield return new WaitForSeconds(2f);
        }
        _isEnemyTurnComplited = true;
    }
}
