using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DamagedCondition", menuName = "ConditionSO/Damaged")]
public class DamagedCondition : ConditionSO
{
    public override bool CheckCondition(EnemyAI ec)
    {
        return ec.currentHealth <= ec.DamagedHP;
    }
}
