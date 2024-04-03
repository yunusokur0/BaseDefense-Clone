using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBulletBoxesCommand
{
    private readonly List<GameObject> _collectableStack;
    public ReturnBulletBoxesCommand(List<GameObject> collectableStack)
    {
        _collectableStack = collectableStack;
    }

    public void Execute()
    {
        var listCount = _collectableStack.Count;
        for (byte i = 0; i < listCount; i++)
        {
            GameObject BulletBox = _collectableStack[_collectableStack.Count - 1];
            _collectableStack.Remove(BulletBox);
            Vector3 targetPosition = BulletBox.transform.localPosition + new Vector3(0f, .5f, 0f);
            BulletBox.transform.DOLocalMove(targetPosition, 0.2f).OnComplete(() =>
            {
                BulletBox.transform.DOLocalMove(new Vector3(0f, 0.5f, -5), 1f).OnComplete(() =>
                {
                    PoolSignals.Instance.onReturnToPool?.Invoke(BulletBox, 0);
                });
            }).SetDelay(i * .05f);
        }
    }
}
