using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MinerManager : MonoBehaviour
{
    [SerializeField] private MinerAnimController minerAnim;
    [SerializeField] private GameObject diamondObj;
    [SerializeField] private GameObject pickaxeObj;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject DiamondGround;
    [SerializeField] private NavMeshAgent agent;

    void Start()
    {
        GoToTarget();
    }
    private void GoToTarget()
    {
        agent.SetDestination(target.transform.position);
        minerAnim.OnChangeAnimationState(AnimEnum.Run);
    }
    private void GoToDiamondGround()
    {
        agent.SetDestination(DiamondGround.transform.position);
        ToggleMiningTools(false);
        minerAnim.OnChangeAnimationState(AnimEnum.Run);
        minerAnim.ToggleCarryLayerWeight();
    }
    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(6.1f);
        GoToDiamondGround();
    }
    private void ToggleMiningTools(bool isActive)
    {
        pickaxeObj.SetActive(isActive);
        diamondObj.SetActive(!isActive);
    }
    public void TriggerTarget()
    {
        StartCoroutine(MoveRoutine());
        ToggleMiningTools(true);
        minerAnim.OnChangeAnimationState(AnimEnum.Dig);
    }
    public void TriggerGround()
    {
        GoToTarget();
        minerAnim.ToggleCarryLayerWeight();
        diamondObj.SetActive(false);
    }
}
