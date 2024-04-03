using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKStackController : MonoBehaviour
{
    public List<GameObject> bulletBoxList;
    [SerializeField] private StackManager stackManager;
    private const float _positionOffset = 0.3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(UnloadCollectedStack());
        if (other.CompareTag("BulletBoxWorker"))
            StartCoroutine(UnloadCollectedStackWorker(other.gameObject));
    }
    public IEnumerator UnloadCollectedStackWorker(GameObject other)
    {
        StackManager stackManager = other.GetComponent<StackManager>();
        while (stackManager._collectedStack.Count > 0)
        {
            GameObject lastChild = stackManager._collectedStack[stackManager._collectedStack.Count - 1];
            BulletBoxAnimated(lastChild);
            stackManager._collectedStack.RemoveAt(stackManager._collectedStack.Count - 1);
            yield return new WaitForSecondsRealtime(.2f);
        }
    }
    public void BulletBoxAnimated(GameObject bulletBox)
    {
        if (bulletBox != null)
        {
            bulletBox.transform.SetParent(gameObject.transform);
            float posX = _positionOffset - (bulletBoxList.Count % 2) * _positionOffset;
            float posZ = _positionOffset - ((bulletBoxList.Count / 2) % 2) * _positionOffset;
            float posY = ((bulletBoxList.Count / 4) * _positionOffset);
            bulletBoxList.Add(bulletBox);

            Vector3 newPosition = new Vector3(posX, posY, posZ);
            bulletBox.transform.DOLocalMove(newPosition, 1);
            bulletBox.transform.rotation = Quaternion.identity;
        }
    }
    public IEnumerator UnloadCollectedStack()
    {
        while (stackManager._collectedStack.Count > 0)
        {
            GameObject lastChild = stackManager._collectedStack[stackManager._collectedStack.Count - 1];
            BulletBoxAnimated(lastChild);
            stackManager._collectedStack.RemoveAt(stackManager._collectedStack.Count - 1);
            yield return new WaitForSecondsRealtime(.2f);
        }
    }
}
