using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseState", menuName = "StatesSO/Chase")]
public class ChaseState : StateSO
{
    public override void OnStateEnter(EnemyAI ec)
    {
    }

    public override void OnStateExit(EnemyAI ec)
    {
        ec.StopGoDestination();
    }

    public override void OnStateUpdate(EnemyAI ec)
    {
        Debug.Log("Ven que te quiero decir una cosa");
        ec.GoDestination();
    }
}
