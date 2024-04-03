using UnityEngine;


public class PlayerAttackPhysicsController : MonoBehaviour
{
    [SerializeField] private PlayerAttackController playerAttackController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerAttackController.enemyObjectStack.Add(other.gameObject);
            playerAttackController.StartCoroutine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerAttackController.enemyObjectStack.Remove(other.gameObject);
            playerAttackController.enemyObjectStack.TrimExcess();
            playerAttackController.StopCorountine();
        }
    }
}
