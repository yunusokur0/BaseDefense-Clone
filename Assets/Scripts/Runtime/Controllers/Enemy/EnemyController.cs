using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamage
{
    public GameObject Target;
    public GameObject TurretTarget;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyAnimController enemyAnimController;
    public EnemyIState CurrentState;
    private EnemyDeadState _deadState;
    private EnemyWalkState _walkState;
    private EnemyPlayerState _playerState;
    private EnemyAttackState _attackState;
    public int heal;
    public IDamage damagee;
    public int Heal { get; set; } = 100;

    public void TakeDamage(int damage)
    {

        Heal -= damage;
        if (Heal <= 0)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            CurrentState = _deadState;
            CurrentState.EnterState();
        }

    }

    public IEnumerator Attack()
    {
       
            yield return new WaitForSeconds(1f);
            if (damagee != null)
            {
                damagee.TakeDamage(5);
                Debug.Log("damage" + damagee);
            }
        
    }
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _deadState = new EnemyDeadState(this);
        _walkState = new EnemyWalkState(this, agent);
        _playerState = new EnemyPlayerState(this, agent);
        _attackState = new EnemyAttackState(this);
    }
    private void Start()
    {
        CurrentState = _walkState;
        CurrentState.EnterState();
    }
    private void Update()
    {
        CurrentState.UpdateState();
    }
    public void CorountineDead()
    {
        StartCoroutine(_deadState.dead());
    }
    public void TriggerAnim(AnimEnum anim)
    {
        enemyAnimController.OnChangeAnimationState(anim);
    }

    public void ChangeState(EnemyStateType state)
    {
        StopAllCoroutines();
        switch (state)
        {
            case EnemyStateType.WalkTarget:
                CurrentState = _walkState;
                break;
            case EnemyStateType.RushTarget:
                CurrentState = _playerState;
                break;
            case EnemyStateType.AttackTarget:
                CurrentState = _attackState;
                break;
            case EnemyStateType.Dead:
                CurrentState = _deadState;
                break;
        }

        CurrentState.EnterState();
    }
}
