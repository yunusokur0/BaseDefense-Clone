using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private CD_Pool _pooldata;
    private PoolInstantiateCommand _poolInstantiateCommand;
    [SerializeField] private List<GameObject> listHolder;

    private void Awake()
    {
        _pooldata = GetPoolData();
        Init();
        _poolInstantiateCommand.Execute();
    }

    private void Init()
    {
        _poolInstantiateCommand = new PoolInstantiateCommand(_pooldata, listHolder);
    }

    private CD_Pool GetPoolData()
    {
        return Resources.Load<CD_Pool>("Data/CD_Pool");
    }

    public GameObject OnGetPoolObject(byte value)
    {
        var itemList = listHolder[value].transform;
        if (itemList.childCount > 0)
        {
            GameObject firstChild = itemList.GetChild(0).gameObject;
            return firstChild;
        }
        else
        {
            Debug.LogWarning("Listede obje yok!");
            return null;
        }
    }


    private void OnReturnToPool(GameObject pooledObject, byte poolIndex)
    {
        pooledObject.SetActive(false);
        pooledObject.transform.position = transform.position;
        pooledObject.transform.parent = transform.GetChild(poolIndex);
    }

    private void OnEnable()
    {
        SubscribeEvent();
    }
    private void SubscribeEvent()
    {
        PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
        PoolSignals.Instance.onReturnToPool += OnReturnToPool;
    }

    private void UnSubscribeEvent()
    {
        PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
        PoolSignals.Instance.onReturnToPool -= OnReturnToPool;
    }
    private void OnDisable()
    {
        UnSubscribeEvent();
    }
}
