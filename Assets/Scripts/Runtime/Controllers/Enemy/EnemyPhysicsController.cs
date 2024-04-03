using UnityEngine;

public class EnemyPhysicsController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        enemyController.CurrentState.OntriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemyController.CurrentState.OntriggerExit(other);
    }
}
