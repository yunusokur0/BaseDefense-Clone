using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class DiamondGroundManager : MonoBehaviour
{
    private const float PositionOffset = 0.3f;
    public List<GameObject> diamondList;
    private void ActivateDiamond()
    {
        GameObject diamond = PoolSignals.Instance.onGetPoolObject?.Invoke(4);
        if (diamond != null)
        {
            diamond.SetActive(true);
            diamond.transform.SetParent(transform);
            float posX = PositionOffset - (diamondList.Count % 3) * PositionOffset;
            float posZ = PositionOffset - ((diamondList.Count / 3) % 3) * PositionOffset;
            float posY = ((diamondList.Count / 9) * PositionOffset);
            diamondList.Add(diamond);
            Vector3 newPosition = new Vector3(posX, posY, posZ);
            diamond.transform.DOLocalMove(newPosition, 0);
        }
    }

    private void MoveAndReturnDiamonds()
    {
        for (byte i = 0; i < diamondList.Count; i++)
        {
            float randomY = Random.Range(1f, 3f);
            float randomZ = Random.Range(-1f, .8f);
            float randomX = Random.Range(-1.2f, 1.2f);
            Vector3 targetPosition = new Vector3(randomX, randomY, randomZ);
            GameObject currentObject = diamondList[i];
            currentObject.transform.DOLocalMove(targetPosition, 1.2f);
            currentObject.transform.DOScale(0, 1.5f).OnComplete(() =>
            {
                PoolSignals.Instance.onReturnToPool.Invoke(currentObject, 4);

            });          
        }
        UISignals.Instance.onSetDiamondValue?.Invoke((byte)diamondList.Count);
        diamondList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Miner"))     
            ActivateDiamond();

        if (other.CompareTag("Player"))
            MoveAndReturnDiamonds();
    }
}
