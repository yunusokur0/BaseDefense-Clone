using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_Pool", menuName = "BaseDefense/CD_Pool", order = 0)]
public class CD_Pool : ScriptableObject
{
    public List<PoolData> PoolData;
}
