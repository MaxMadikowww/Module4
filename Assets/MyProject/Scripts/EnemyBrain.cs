using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private MelleNavMover mover;
    [SerializeField] private EnemyAttacker attacker;
    [SerializeField] private float MaxVisibleDistance;
    [SerializeField] private float KoefficentOfSpeed;
    [SerializeField] private float timerCount;

    private Health player;

    private float timer;

    private float Event = 1;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().GetComponent<Health>();
        ResetTimer();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < MaxVisibleDistance)
        {
            if (attacker.TargetInRange(player.transform))
            {
                mover.Stop();
                attacker.Attack();
            }
            else
            {
                mover.MoveTo(player.transform.position);
            }
            Event = 1;
            ResetTimer();
        }
        else
        {
            if (Event == 1) 
            {
                mover.Stop();
                RemoveTimer();
                if (timer < 0)
                {
                    Event = 2;
                }
            }
            if (Event == 2) 
            {
                if (timer < 0)
                {
                    mover.MoveTo(GetDirection());
                    ResetTimer();
                }
                RemoveTimer();
            }
        }
    }
    private void ResetTimer() => timer = timerCount;
    private void RemoveTimer() => timer -= Time.deltaTime;
    private Vector3 GetDirection()
    {
        var direction = Random.Range(0,4);
        switch (direction)
        {
            case 0:
                return Vector3.forward * KoefficentOfSpeed + transform.position;
            case 1:
                return Vector3.back * KoefficentOfSpeed + transform.position;
            case 2:
                return Vector3.left * KoefficentOfSpeed + transform.position;
            case 3:
                return Vector3.right * KoefficentOfSpeed + transform.position;
            default:
                return Vector3.zero;
        }
    }
}
