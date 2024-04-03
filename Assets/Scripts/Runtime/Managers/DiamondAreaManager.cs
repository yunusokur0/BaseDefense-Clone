using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamondAreaManager : MonoBehaviour
{
    public List<GameObject> MinerList;
    public List<GameObject> CaptiveMiner;
    public TextMeshPro GemMineText;
    private byte GemMineValue;
    private GameObject OnGetMiner()
    {
        var lastIndex = Random.Range(0, MinerList.Count); 
        GameObject miner = MinerList[lastIndex];
        MinerList.RemoveAt(lastIndex);
        return miner;
    }

    public void GemMine(byte mine)
    {
        GemMineValue += mine;
        GemMineText.text = (GemMineValue + " / 10").ToString();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        DiamondAreaSignals.Instance.onGetMiner += OnGetMiner;
    }
    private void UnSubscribeEvents()
    {
        DiamondAreaSignals.Instance.onGetMiner -= OnGetMiner;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
