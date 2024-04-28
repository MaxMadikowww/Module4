using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeNavMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator Animator;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float range;

    Transform player;
    Health health;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if (!health.isDead)
        {
            if (agent.remainingDistance < 1)
            {
                agent.isStopped = true;
                Animator.SetFloat("Movement", 0);
                SetNewPoint();
            }
            else
            {
                agent.isStopped = false;
                Animator.SetFloat("Movement", rigid.velocity.magnitude);
            }
        }
    }
    private void SetNewPoint()
    {
        Vector3 point = FindRandomPoint();
        agent.SetDestination(point);
    }
    private Vector3 FindRandomPoint()
    {
        int count = 5;
        Vector3 position;
        do
        {
            Vector2 point = Random.insideUnitCircle;

            if (Mathf.Abs(point.x) < 0.5f)
            {
                point.x *= 2;
            }
            if (Mathf.Abs(point.y) < 0.5f)
            {
                point.y *= 2;
            }
            position = new Vector3(point.x, 0, point.y) * range;

            position += player.position;
            count--;
        }
        while (!agent.Raycast(position, out var hit) && count > 0);
        
        return position;
    }
}
