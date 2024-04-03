using UnityEngine;
using UnityEngine.AI;

public class CollectableMinerManager : MonoBehaviour
{
    [SerializeField] private DiamondAreaManager diamondAreaManager;
    [SerializeField] private MinerAnimController anim;
    [SerializeField] private NavMeshAgent agent;

    private GameObject _target;
    private BuildEnum _buildEnum = BuildEnum.Close;
    private GameObject miner;
   
    private void Start()
    {
        miner = DiamondAreaSignals.Instance.onGetMiner?.Invoke();
    }
    private void Update()
    {
        CheckAndSetTarget();
        UpdateAnimation();
    }
    private void CheckAndSetTarget()
    {
        if (_target != null)
        {
            agent.SetDestination(_target.transform.position);
        }
    }

    private void UpdateAnimation()
    {
        bool isMoving = _target != null && agent.remainingDistance > agent.stoppingDistance;
        anim.SetBoolAnim(AnimEnum.Run, isMoving);
    }
    public void TargetReturn(GameObject other)
    {
        if (diamondAreaManager.CaptiveMiner.Count <= 0)
        {
            _target = other;
        }
        else
        {
            _target = diamondAreaManager.CaptiveMiner[diamondAreaManager.CaptiveMiner.Count - 1];
        }
    }
    private void OnSetMiner()
    {
        miner.transform.position = gameObject.transform.position;
        miner.transform.rotation = gameObject.transform.rotation;
        miner.SetActive(true);

        diamondAreaManager.CaptiveMiner.Remove(gameObject);
        diamondAreaManager.CaptiveMiner.TrimExcess();
        diamondAreaManager.GemMine(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_buildEnum == BuildEnum.Close)
            {
                TargetReturn(other.gameObject);
                diamondAreaManager.CaptiveMiner.Add(gameObject);
                _buildEnum = BuildEnum.Open;
            }
        }
    }

    private void OnEnable() => SubscribeEvents();
    private void SubscribeEvents()
    {
        DiamondAreaSignals.Instance.onSetMiner += OnSetMiner;
    }
    private void UnSubscribeEvents()
    {
        DiamondAreaSignals.Instance.onSetMiner -= OnSetMiner;
    }
    private void OnDisable() => UnSubscribeEvents();
}
