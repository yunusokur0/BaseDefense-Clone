using System.Collections;
using UnityEngine;

public class PKManager : MonoBehaviour
{
    #region Variable
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private PKController pkController;
    [SerializeField] private PKStackController pkStackControll;
    [SerializeField] private PKAutomaticBuyController pkAutomaticBuyController;
    [SerializeField] private Collider collider;
    private const byte BulletSpeed = 4;
    private const float BulletSpawnRate = 0.5f;
    private const float BulletDeactivationDelay = 1f;
    private const byte MaxBulletsBeforeDeactivation = 4;
    private byte bulletCount;
    #endregion
    private void Start()
    {
        InvokeRepeating(nameof(AutoSpawmBullet), 0, BulletSpawnRate);
    }
    void Update()
    {
        LockTarget();
        ColliderDeactive();

    }
    private void ColliderDeactive()
    {
        if (pkAutomaticBuyController.buildStatus == BuildEnum.Open)
            collider.enabled = false;

    }
    public void AutoSpawmBullet()
    {
        if (CanSpawnBullet(BuildEnum.Open))
        {
            var bullet = PoolSignals.Instance.onGetPoolObject?.Invoke(1);
            SetBulletProperties(bullet);
            StartCoroutine(DeactivateBullet(bullet));
            IncreaseBulletCount();
        }
    }
    public IEnumerator SpawnBullet()
    {
        while (CanSpawnBullet(BuildEnum.Close))
        {
            var bullet = PoolSignals.Instance.onGetPoolObject?.Invoke(1);
            SetBulletProperties(bullet);
            StartCoroutine(DeactivateBullet(bullet));
            IncreaseBulletCount();
            yield return new WaitForSeconds(BulletSpawnRate);
        }
    }
    private void LockTarget()
    {
        if (CanSpawnBullet(BuildEnum.Open))
        {
            Vector3 direction = pkController.EnumList[0].transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    }
    private bool CanSpawnBullet(BuildEnum en)
    {
        return pkController.EnumList.Count > 0 && pkStackControll.bulletBoxList.Count > 0 && pkAutomaticBuyController.buildStatus == en;
    }

    private void SetBulletProperties(GameObject bullet)
    {
        bullet.transform.position = bulletTransform.position;
        Quaternion currentRotation = bullet.transform.rotation;
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotation.eulerAngles.z);
        bullet.transform.rotation = newRotation;
        bullet.SetActive(true);
        bullet.transform.SetParent(gameObject.transform.parent.parent);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * BulletSpeed;
    }

    private IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(BulletDeactivationDelay);
        PoolSignals.Instance.onReturnToPool?.Invoke(bullet, 1);
    }
    private void IncreaseBulletCount()
    {
        bulletCount++;
        if (bulletCount == MaxBulletsBeforeDeactivation)
        {
            GameObject firstChild = pkStackControll.bulletBoxList[pkStackControll.bulletBoxList.Count - 1];
            pkStackControll.bulletBoxList.Remove(firstChild);
            PoolSignals.Instance.onReturnToPool?.Invoke(firstChild, 0);
            bulletCount = 0;
        }
    }
}
