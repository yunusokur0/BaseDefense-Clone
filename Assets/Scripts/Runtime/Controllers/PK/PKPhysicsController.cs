using UnityEngine;


public class PKPhysicsController : MonoBehaviour
{
    [SerializeField] private PKController pkController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            pkController.EnumList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            pkController.EnumList.Remove(other.gameObject);
            pkController.EnumList.TrimExcess();
        }
    }
}
