using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "DieState", menuName = "StatesSO/Die")]
public class DieState : StateSO
{
    public override void OnStateEnter(EnemyAI ec)
    {
        Debug.Log("Abandoné este mundo de miseria y desesperación");
        GameObject.Destroy(ec.gameObject);
    }

    public override void OnStateExit(EnemyAI ec)
    {
    }

    public override void OnStateUpdate(EnemyAI ec)
    {
    }
}
