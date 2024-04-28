using Assets.MyProject.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float attackCD;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask attackedMask;
    [SerializeField] private InputManager input;

    private float attackTime;
    private Collider[] enemies = new Collider[1];
    private Health health;
    private bool CanAttack => attackTime <= 0;

    private void Start()
    {
        ResetTime();
        health = GetComponent<Health>();
    }
    public bool TargetInRange(Transform target) => 
        Vector3.Distance(transform.position, target.position) < radius;
    
    public void Attack()
    {
        if (!health.isDead)
        {
            if (CanAttack)
            {
                Animator.SetTrigger("Attack");
                Animator.SetInteger("AttackIndex", 0);
                ResetTime();
                AttackNearEnemies();
            }
        }
    }

    private void Update() => attackTime -= Time.deltaTime;
    private void ResetTime() => attackTime = attackCD;
    private void AttackNearEnemies()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, enemies, attackedMask);
        if (count > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].TryGetComponent<Health>(out var health))
                {
                    health.GetDamage(damage);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
    }
}
