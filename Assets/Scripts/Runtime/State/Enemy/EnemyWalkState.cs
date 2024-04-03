using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkState : EnemyIState
{
    private readonly EnemyController _enemy;
    private readonly NavMeshAgent _agent;
    public EnemyWalkState(EnemyController enemy, NavMeshAgent agent)
    {
        _enemy = enemy;
        _agent = agent;
    }
    public void EnterState()
    {
        _enemy.TurretTarget = _enemy.Target;
        _agent.speed = .75f;
        _agent.SetDestination(_enemy.Target.transform.position);
        _enemy.TriggerAnim(AnimEnum.Walking);
    }

    public void OntriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.Target = other.gameObject;
            _enemy.ChangeState(EnemyStateType.RushTarget);
            _enemy.damagee = other.GetComponent<IDamage>();
        }
    }

    public void OntriggerExit(Collider other)
    {
        Debug.Log("OntriggerExit");
    }

    public void UpdateState()
    {
        if ((_enemy.transform.position - _enemy.Target.transform.position).sqrMagnitude <= Mathf.Pow(_agent.stoppingDistance, 1))
        {
            _enemy.ChangeState(EnemyStateType.AttackTarget);
        }
    }
}
