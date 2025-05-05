using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleState", menuName = "StatesSO/Idle")]
public class IdleState : StateSO
{
    public override void OnStateEnter(EnemyAI ec)
    {
        
    }

    public override void OnStateExit(EnemyAI ec)
    {
    }

    public override void OnStateUpdate(EnemyAI ec)
    {
        Debug.Log("Here chillin");
        ec.GetComponent<PatrollIDLE>().Patrol();
    }
}
