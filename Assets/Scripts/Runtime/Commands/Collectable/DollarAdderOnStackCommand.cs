using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class DollarAdderOnStackCommand
{
    private readonly List<GameObject> _collectableStack;
    private const float yPos = .36f;
    private const float yPosValue = .06f;
    private const float zPosValue = -0.12f;
    public DollarAdderOnStackCommand(List<GameObject> collectableStack)
    {
        _collectableStack = collectableStack;
    }
    public void Execute(GameObject collectableGameObject, GameObject stackManager)
    {
        collectableGameObject.transform.SetParent(stackManager.transform);
        _collectableStack.Add(collectableGameObject);
        collectableGameObject.GetComponent<Rigidbody>().useGravity = false;
        collectableGameObject.GetComponent<Rigidbody>().isKinematic = true;
        collectableGameObject.GetComponent<Collider>().enabled = false;
        float posY = yPos + ((_collectableStack.Count) * yPosValue);
        Vector3 newpos = new Vector3(0, posY, zPosValue);
        collectableGameObject.transform.DOLocalRotate(Vector3.zero, 1);
        collectableGameObject.transform.DOLocalMove(newpos, 1f);
    }
}
