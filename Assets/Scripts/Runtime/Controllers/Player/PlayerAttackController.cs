using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackController : MonoBehaviour
{
    public List<GameObject> enemyObjectStack = new List<GameObject>();
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Transform runningtransform;
    [SerializeField] private GameObject levelHolder;

    public void StartCoroutine()
    {
        StartCoroutine(Fire());
    }
    public void StopCorountine()
    {
        StopAllCoroutines();
    }
    public IEnumerator Fire()
    {
        if (enemyObjectStack.Count > 0)
        {
            while (true)
            {
                var playerBullet = PoolSignals.Instance.onGetPoolObject?.Invoke(2);
                SetBulletProperties(playerBullet);
                StartCoroutine(DeactivateBullet(playerBullet));
                yield return new WaitForSeconds(0.5f);
            }
        }        
    }

    private void SetBulletProperties(GameObject playerBullet)
    {
        playerBullet.transform.position = fireTransform.position;
        Quaternion currentRotation = playerBullet.transform.rotation;
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, runningtransform.rotation.eulerAngles.y, currentRotation.eulerAngles.z);
        playerBullet.transform.rotation = newRotation;
        playerBullet.SetActive(true);
        playerBullet.transform.SetParent(levelHolder.transform);
        Rigidbody rb = playerBullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 5;
    }

    private IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        PoolSignals.Instance.onReturnToPool?.Invoke(bullet, 1);
    }
}
