using System.Collections.Generic;
using UnityEngine;

public class PKController : MonoBehaviour
{
    public List<GameObject> EnumList = new List<GameObject>();

    private void RemoveEnemyFromList(GameObject Enemy)
    {
        EnumList.Remove(Enemy);
        EnumList.TrimExcess();
    }

    private void OnEnable() => SubscribeEvents();
    private void SubscribeEvents()
    {
        StackSignals.Instance.onRemoveEnemyFromList += RemoveEnemyFromList;
    }
    private void UnSubscribeEvents()
    {
        StackSignals.Instance.onRemoveEnemyFromList -= RemoveEnemyFromList;
    }
    private void OnDisable() => UnSubscribeEvents();
}
