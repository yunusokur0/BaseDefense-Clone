using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMoneyComman
{
    private readonly List<GameObject> _collectableStack;
    public ReturnMoneyComman(List<GameObject> collectableStack)
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

            BulletBox.transform.DOLocalMove(new Vector3(Random.Range(-.3f, .3f), Random.Range(1f, 1.7f), Random.Range(-.45f, .2f)), .6f).OnComplete(() =>
             {
                 BulletBox.transform.DOScale(0, .3f).OnComplete(() =>
                 {
                     PoolSignals.Instance.onReturnToPool?.Invoke(BulletBox, 0);
                 });
             });
        }
    }
}
