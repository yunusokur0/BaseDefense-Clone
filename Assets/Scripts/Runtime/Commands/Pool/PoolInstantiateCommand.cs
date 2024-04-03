using System.Collections.Generic;
using UnityEngine;

public class PoolInstantiateCommand
{
    private readonly CD_Pool _data;
    private readonly List<GameObject> _layers = new List<GameObject>();
    public PoolInstantiateCommand(CD_Pool poolData, List<GameObject> layers)
    {
        _data = poolData;
        _layers = layers;
    }

    public void Execute()
    {
        var data = _data.PoolData;
        for (int i = 0; i < _layers.Count; i++)
        {
            for(int j = 0; j <data[i].Count;j++)
            {
                GameObject spawnedObject = Object.Instantiate(data[i].Prefabs, _layers[i].transform);
                spawnedObject.SetActive(false);
            }         
        }
    }
}
