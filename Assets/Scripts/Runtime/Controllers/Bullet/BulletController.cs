using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            IDamage damage = other.GetComponent<IDamage>();
            damage.TakeDamage(50);
            PoolSignals.Instance.onReturnToPool?.Invoke(gameObject, 1);
        }

        if (other.CompareTag("Boss"))
        {
            IDamage damage = other.GetComponent<IDamage>();
            damage.TakeDamage(20);
            PoolSignals.Instance.onReturnToPool?.Invoke(gameObject, 1);
        }
    }
}
