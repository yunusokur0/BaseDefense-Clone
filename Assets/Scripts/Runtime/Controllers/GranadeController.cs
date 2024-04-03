using System.Collections;
using UnityEngine;


public class GranadeController : MonoBehaviour
{
    public GameObject Granade;
    public ParticleSystem partical;

    public Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ShotPoint"))
        {
            Granade.SetActive(false);
            partical.Play();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        if(other.CompareTag("Player"))
        {
            other.GetComponent<IDamage>().TakeDamage(40);
        }
    }
}
