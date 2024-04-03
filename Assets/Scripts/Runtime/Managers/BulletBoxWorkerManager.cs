using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class BulletBoxWorkerManager : MonoBehaviour
{
    #region variable;
    [SerializeField] private GameObject ammon;
    [SerializeField] private List<GameObject> pk;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private StackManager stackManager;
    [SerializeField] private BulletBoxWorkerAnimController animController;

    private const float FillStackDelay = 0.4f;
    private const float UnloadStackDelay = 0.75f;
    private const int MaxStackCount = 3;

    #endregion
    private void Start()
    {
        GoToAmmon();
    }
    private void Update()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        bool isMoving = !agent.pathPending && agent.remainingDistance > agent.stoppingDistance;
        animController.SetBoolAnim(AnimEnum.Run, isMoving);
    }

    private void GoToAmmon()
    {
        if (ammon != null)
        {
            agent.SetDestination(ammon.transform.position);
        }
    }

    private IEnumerator FillBulletBoxStack()
    {
        while (stackManager._collectedStack.Count < MaxStackCount)
        {
            var bulletBox = PoolSignals.Instance.onGetBulletBox?.Invoke();
            if (bulletBox != null)
            {
                stackManager._bulletBoxAdderOnStackCommand.Execute(bulletBox);
            }
            yield return new WaitForSeconds(FillStackDelay);
        }
        GoToRandomActivePK();
    }

    private IEnumerator UnloadCollectedStack()
    {
        yield return new WaitForSeconds(UnloadStackDelay);
        GoToAmmon();
    }
    private void GoToRandomActivePK()
    {
        var activePKs = pk.Where(pkObj => pkObj.activeSelf).ToList();

        if (activePKs.Any())
        {
            int randomIndex = Random.Range(0, activePKs.Count);
            agent.SetDestination(activePKs[randomIndex].transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammon"))
        {
            StartCoroutine(FillBulletBoxStack());
        }
        else if (other.CompareTag("PKStack"))
        {
            StartCoroutine(UnloadCollectedStack());
        }
    }
}
