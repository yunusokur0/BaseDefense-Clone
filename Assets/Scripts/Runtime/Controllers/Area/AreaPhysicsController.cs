using UnityEngine;

public class AreaPhysicsController : MonoBehaviour
{
    [SerializeField] private AreaManager areaManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            areaManager.StartDecreaseValueCoroutine();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            areaManager.StopAllCoroutinesSafe();
        }
    }
}
