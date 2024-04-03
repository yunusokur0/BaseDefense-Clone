using UnityEngine;

public interface EnemyIState
{
    void EnterState();
    void UpdateState();
    void OntriggerEnter(Collider other);
    void OntriggerExit(Collider other);
}
