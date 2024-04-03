using UnityEngine;

public class AmmonAreaManager : MonoBehaviour
{
    public GameObject onGetBulletBox()
    {
        var firstChild = PoolSignals.Instance.onGetPoolObject?.Invoke(0);
        firstChild.transform.position = transform.position;
        firstChild.SetActive(true);
        return firstChild;
    }

    private void OnEnable()
    {
        SubscribeEvent();
    }
    private void SubscribeEvent()
    {
        PoolSignals.Instance.onGetBulletBox += onGetBulletBox;
    }
    private void UnSubscribeEvent()
    {
        PoolSignals.Instance.onGetBulletBox -= onGetBulletBox;
    }
    private void OnDisable()
    {
        UnSubscribeEvent();
    }
}
