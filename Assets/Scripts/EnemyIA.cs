using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject player;
    public int DamagedHP;
    public bool OnRange = false, OnAttackRange = false;
    public StateSO currentNode;
    public List<StateSO> Nodes;
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Character");
        currentHealth = maxHealth;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnRange = true;
            InvokeRepeating(nameof(DamagePlayer), 0f, 1f); // Empieza a restar vida cada segundo
        }
        CheckEndingConditions();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnRange = false;
            CancelInvoke(nameof(DamagePlayer)); // Detiene el daño al salir del área
            if (currentHealth != maxHealth)
            {
                GetComponent<PatrollIDLE>().Patrolpoint.transform.position = new Vector3(player.transform.position.x, 1.21f, player.transform.position.z);
                GetComponent<PatrollIDLE>().isPatrolSearching = true;
            }
        }

        
        CheckEndingConditions();
    }

    private void DamagePlayer()
    {
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Resta 1 de vida por segundo
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            OnAttackRange = true;
        CheckEndingConditions();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            OnAttackRange = false;
        CheckEndingConditions();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth--;
        }
        CheckEndingConditions();
        currentNode.OnStateUpdate(this);
    }
    public void CheckEndingConditions()
    {
        foreach (ConditionSO condition in currentNode.EndConditions)
            if (condition.CheckCondition(this) == condition.answer) ExitCurrentNode();
    }
    public void ExitCurrentNode()
    {
        foreach (StateSO stateSO in Nodes)
        {
            if (stateSO.StartCondition == null)
            {
                EnterNewState(stateSO);
                break;
            }
            else
            {
                if (stateSO.StartCondition.CheckCondition(this) == stateSO.StartCondition.answer)
                {
                    EnterNewState(stateSO);
                    break;
                }
            }
        }
        currentNode.OnStateEnter(this);
    }
    private void EnterNewState(StateSO state)
    {
        currentNode.OnStateExit(this);
        currentNode = state;
        currentNode.OnStateEnter(this);
    }

    public void GoDestination()
    {
           agent.SetDestination(player.transform.position);
    }

    public void StopGoDestination()
    {
        agent.ResetPath();
    }

    public void RunPlayer(Transform target, Transform origin)
    {
        Vector3 direction = (origin.position - target.position).normalized;
        Vector3 fleeposition = origin.position + direction;
        agent.SetDestination(fleeposition);
    }
}
