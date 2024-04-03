using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public List<GameObject> moneyList;
    public void OnAddMoney(GameObject money)
    {
        moneyList.Add(money);
    }

    private GameObject OnGetFirstMoney()
    {
        if (moneyList.Count > 0)
        {
            GameObject firstMoney = moneyList[0].gameObject;
            return firstMoney;
        }
        else
        {
            Debug.LogWarning("Listede obje yok!");
            return null;
        }
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        StackSignals.Instance.onGetFirstMoney += OnGetFirstMoney;
        StackSignals.Instance.onAddMoney += OnAddMoney;
    }

    private void UnSubscribeEvents()
    {
        StackSignals.Instance.onGetFirstMoney -= OnGetFirstMoney;
        StackSignals.Instance.onAddMoney -= OnAddMoney;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
