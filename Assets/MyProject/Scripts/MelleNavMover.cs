using UnityEngine;
using UnityEngine.AI;

public class MelleNavMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator Animator;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float range;

    private Health health;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    public void Stop()
    {
        UpdateSpeed(0);
    }

    public void MoveTo(Vector3 position)
    {
        if (!health.isDead)
        {

            agent.SetDestination(position);
            UpdateSpeed(rigid.velocity.magnitude);
        }
    }
    private void UpdateSpeed(float speed)
    {
        if (speed <= 0)
        {
            agent.isStopped = true;
            Animator.SetFloat("Movement", 0);
            agent.ResetPath();
        }
        else
        {
            agent.isStopped = false;
            Animator.SetFloat("Movement", speed);
        }
    }
}
