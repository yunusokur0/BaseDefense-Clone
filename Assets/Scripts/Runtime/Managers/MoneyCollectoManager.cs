using UnityEngine;
using UnityEngine.AI;

public class MoneyCollectoManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject baseTarget;
    [SerializeField] private MoneyController moneyController;
    [SerializeField] private StackManager stackManager;
    [SerializeField] private BulletBoxWorkerAnimController animController;
    private DollarAdderOnStackCommand _dollarAdderOnStackCommand;


    public void OnInteractionDollarCollectable(GameObject collectableGameObject)
    {
        if (stackManager._collectedStack.Count < 25)
        {
            _dollarAdderOnStackCommand.Execute(collectableGameObject, gameObject);
            moneyController.moneyList.Remove(collectableGameObject);
            moneyController.moneyList.TrimExcess();
            target = baseTarget;
        }
    }

    private void Awake()
    {
        _dollarAdderOnStackCommand = new DollarAdderOnStackCommand(stackManager._collectedStack);
    }

    private void Update()
    {
        money = StackSignals.Instance.onGetFirstMoney?.Invoke();
        target = money != null ? money : baseTarget;
        agent.SetDestination(target.transform.position);
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        bool isMoving = !agent.pathPending && agent.remainingDistance > agent.stoppingDistance;
        animController.SetBoolAnim(AnimEnum.Run, isMoving);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DollarCollectable"))
        {
            OnInteractionDollarCollectable(other.gameObject);
        }

        if (other.CompareTag("Door"))
        {
            stackManager.MoneyorBulletBoxAnim();
        }
    }
}
