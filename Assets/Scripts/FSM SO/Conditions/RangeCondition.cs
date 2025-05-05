using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OnRange", menuName = "ConditionSO/Range")]
public class RangeCondition : ConditionSO
{
    public override bool CheckCondition(EnemyAI ec)
    {
        return ec.OnRange;
    }

}
