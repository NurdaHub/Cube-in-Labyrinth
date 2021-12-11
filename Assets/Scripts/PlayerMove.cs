using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform finishTransform;
    [SerializeField] private ParticleSysController particleSysController;
    
    private NavMeshAgent navMeshAgent;
    private string finishTag = "Finish";

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetAgentDestination()
    {
        navMeshAgent.SetDestination(finishTransform.position);
    }

    private void Finish()
    {
        particleSysController.PlayParticles();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(finishTag))
            Finish();
    }
}
