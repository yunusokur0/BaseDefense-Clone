using System.Collections;
using UnityEngine;

public class EnemyDeadState : EnemyIState
{
    private readonly EnemyController _enemy;
    public EnemyDeadState(EnemyController enemy)
    {
        _enemy = enemy;
    }
    public void EnterState()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject money = PoolSignals.Instance.onGetPoolObject?.Invoke(5);
            money.transform.SetParent(_enemy.transform.parent);
            money.SetActive(true);
            Vector3 pos = _enemy.transform.localPosition;
            money.transform.localPosition = pos;
            money.GetComponent<Rigidbody>().AddForce(new Vector3(0, Random.Range(2.5f, 5f), 0), ForceMode.Impulse);
            StackSignals.Instance.onAddMoney?.Invoke(money);
        }
        _enemy.CorountineDead();
    }
    public IEnumerator dead()
    {
        StackSignals.Instance.onRemoveEnemyFromList?.Invoke(_enemy.gameObject);
        _enemy.TriggerAnim(AnimEnum.Dead);
        yield return new WaitForSeconds(2f);
       PoolSignals.Instance.onReturnToPool?.Invoke(_enemy.gameObject,3);
    }

    public void UpdateState()
    {
        Debug.Log("UpdateState");
    }
    public void OntriggerEnter(Collider other)
    {
        Debug.Log("OntriggerEnter");
    }
    public void OntriggerExit(Collider other)
    {
        Debug.Log("OntriggerExit");
    }
}
