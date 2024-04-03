using UnityEngine;

public class BorderAreaPhyiscsyController : MonoBehaviour
{
    [SerializeField] private BorderAreaManager borderAreaManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            borderAreaManager.StartCor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            borderAreaManager.StopCoroutine();
        }
    }
}
