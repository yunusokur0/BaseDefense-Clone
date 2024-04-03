using UnityEngine;


public class PKAreaPhysicsController : MonoBehaviour
{
    [SerializeField] private PKAreaManager pkAreaManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pkAreaManager.StartDecreaseNumberCoroutine();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pkAreaManager.StopAllCoroutinesSafe();
        }
    }
}
