using Assets.MyProject.Scripts;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float attackCD;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask attackedMask;
    [SerializeField] private InputManager input;

    private float attackTime;
    private Collider[] enemies = new Collider[3];
    private Health health;
    private bool CanAttack => attackTime <= 0;

    private void Start()
    {
        ResetTime();
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if (!health.isDead)
        {
            if (!CanAttack) attackTime -= Time.deltaTime;

            if (input.AttackPressed && CanAttack)
            {
                var attackNum = Random.Range(0, 2);
                Animator.SetTrigger("Attack");
                Animator.SetInteger("AttackIndex", attackNum);
                ResetTime();
                AttackNearEnemies();
            }
        }
    }
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
