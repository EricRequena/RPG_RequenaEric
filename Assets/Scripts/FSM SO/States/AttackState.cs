using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackState", menuName = "StatesSO/Attack")]
public class AttackState : StateSO
{
    public override void OnStateEnter(EnemyAI ec)
    {
    }

    public override void OnStateExit(EnemyAI ec)
    {

    }

    public override void OnStateUpdate(EnemyAI ec)
    {
        Debug.Log("Te reviento a chancletaso");

 
        if (ec.OnAttackRange)
        {
            PlayerHealth playerHealth = ec.player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1 * Time.deltaTime); // Resta vida por segundo

                if (playerHealth.currentHealth <= 0)
                {
                    GameObject.Destroy(ec.player); // Destruye al jugador si su vida es 0
                }
            }
        }
    }
}
