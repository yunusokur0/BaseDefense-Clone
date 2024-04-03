using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour, IDamage
{
    public float minThrowSpeed;
    public float maxThrowSpeed;
    public float minLaunchAngle;
    public float maxLaunchAngle;
    public BossAnimController bossAnimController;

    [SerializeField] private GameObject shotPoint;
    [SerializeField] private GameObject granadetrans;
    [SerializeField] private Transform player;
    [SerializeField] private BossHealthController bossHealthController;

    private GameObject _bomb;
    private int _heal = 500;
    public int Heal
    {
        get => _heal;
        set
        {
            _heal = value;
            bossHealthController.UpdateHealScaleText(value);
        }
    }

    public void ThrowBomb1()
    {
        _bomb = PoolSignals.Instance.onGetPoolObject(6);
        _bomb.SetActive(true);
        _bomb.transform.position = granadetrans.transform.position;
        _bomb.transform.parent = gameObject.transform.parent;
        Vector3 targetDelta = player.position - gameObject.transform.position;

        gameObject.transform.LookAt(player.transform.position);

        float distance = targetDelta.magnitude;

        float throwSpeed = Mathf.Lerp(minThrowSpeed, maxThrowSpeed, distance / 7f);
        float launchAngle = Mathf.Lerp(minLaunchAngle, maxLaunchAngle, distance / 7f);
        float angleRad = launchAngle * Mathf.Deg2Rad;

        float velocityX = throwSpeed * Mathf.Cos(angleRad);
        float velocityY = throwSpeed * Mathf.Sin(angleRad);
        Vector3 velocityVec = targetDelta.normalized * velocityX;
        velocityVec.y = velocityY;
        _bomb.GetComponent<Rigidbody>().velocity = velocityVec;
    }


    public void SetShotPointToPlayerPosition()
    {
        shotPoint.transform.position = player.transform.position;
        shotPoint.SetActive(true);
    }

    public IEnumerator PerformAttack()
    {
        while (true)
        {
            bossAnimController.OnChangeAnimationState(AnimEnum.Attack);
            yield return new WaitForSeconds(1f);
            SetShotPointToPlayerPosition();
            ThrowBomb1();
            yield return new WaitForSeconds(2);
            PoolSignals.Instance.onReturnToPool?.Invoke(_bomb, 6);
            shotPoint.SetActive(false);
        }
    }

    public void StartAttack()
    {
        StartCoroutine(PerformAttack());
    }

    public void StopAllCoroutines()
    {
        StopAllCoroutines();
    }

    public void TakeDamage(int damage)
    {
        Heal -= damage;
        if (Heal <= 0)
        {
            Heal = 0;
        }
    }
}


