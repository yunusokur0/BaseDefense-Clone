using UnityEngine;

public class MinerPhysicsController : MonoBehaviour
{
    [SerializeField] private MinerManager minerManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DiamondTarget"))
        {
            minerManager.TriggerTarget();
        }

        else if (other.CompareTag("DiamondGround"))
        {
            minerManager.TriggerGround();
        }
    }
}
