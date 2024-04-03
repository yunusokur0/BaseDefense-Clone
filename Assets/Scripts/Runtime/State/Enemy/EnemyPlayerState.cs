using UnityEngine;
using UnityEngine.AI;

public class EnemyPlayerState : EnemyIState
{
    private readonly EnemyController _enemy;
    private readonly NavMeshAgent _agent;
    public EnemyPlayerState(EnemyController enemy, NavMeshAgent agent)
    {
        _enemy = enemy;
        _agent = agent;
    }
    public void EnterState()
    {
        _enemy.TriggerAnim(AnimEnum.Run);
        _agent.speed = 1.15f;
    }

    public void OntriggerEnter(Collider other)
    {
        Debug.Log("OntriggerEnter");
    }

    public void OntriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _enemy.ChangeState(EnemyStateType.WalkTarget);
    }

    public void UpdateState()
    {
        _agent.SetDestination(_enemy.Target.transform.position);
        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            _enemy.ChangeState(EnemyStateType.AttackTarget);
        }
    }
}
