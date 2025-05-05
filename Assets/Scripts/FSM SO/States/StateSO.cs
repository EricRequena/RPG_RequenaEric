using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public ConditionSO StartCondition;
    public List<ConditionSO> EndConditions;
    public abstract void OnStateEnter(EnemyAI ec);
    public abstract void OnStateUpdate(EnemyAI ec);
    public abstract void OnStateExit(EnemyAI ec);

}
