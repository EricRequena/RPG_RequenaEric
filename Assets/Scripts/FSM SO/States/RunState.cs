using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RunState", menuName = "StatesSO/Run")]
public class RunState : StateSO
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
        Debug.Log("CoSorro");
        ec.RunPlayer(ec.player.transform, ec.transform);
    }
}
