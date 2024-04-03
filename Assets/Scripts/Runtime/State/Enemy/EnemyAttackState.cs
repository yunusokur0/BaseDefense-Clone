using UnityEngine;

public class EnemyAttackState : EnemyIState
{
    private readonly EnemyController _enemy;
    public EnemyAttackState(EnemyController enemy)
    {
        _enemy = enemy;
    }
    public void EnterState()
    {
        _enemy.TriggerAnim(AnimEnum.Attack);
        _enemy.StartCoroutine(_enemy.Attack());
    }
    public void OntriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.damagee = other.GetComponent<IDamage>();
            _enemy.Target = other.gameObject;
            _enemy.ChangeState(EnemyStateType.RushTarget);
        }
    }

    public void OntriggerExit(Collider other)
    {
        _enemy.ChangeState(EnemyStateType.RushTarget);
    }

    public void UpdateState()
    {
        if (_enemy.heal>=2)
        {
            _enemy.ChangeState(EnemyStateType.Dead);
        }
    }
}
