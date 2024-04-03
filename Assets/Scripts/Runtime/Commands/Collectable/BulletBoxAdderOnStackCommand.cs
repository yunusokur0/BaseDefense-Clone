using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoxAdderOnStackCommand
{
    private StackManager _stackManager;
    private readonly List<GameObject> _collectableStack;
    private const float yPosValue = .25f;
    private const float zPosValue = -0.2f;
    public BulletBoxAdderOnStackCommand(StackManager stackManager, List<GameObject> collectableStack)
    {
        _stackManager = stackManager;
        _collectableStack = collectableStack;
    }
    public void Execute(GameObject collectableGameObject)
    {
        collectableGameObject.transform.SetParent(_stackManager.transform);
        _collectableStack.Add(collectableGameObject);
        float posY = ((_collectableStack.Count) * yPosValue);
        Vector3 newpos = new Vector3(0, posY, zPosValue);
        collectableGameObject.transform.DOLocalRotate(Vector3.zero, .8f);
        collectableGameObject.transform.DOLocalMove(newpos, .8f);
    }
}