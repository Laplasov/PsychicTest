using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBattleState
{
    public abstract void EnteringState(BattleLogic logic);
    public abstract void UpdateState(BattleLogic logic);

}


