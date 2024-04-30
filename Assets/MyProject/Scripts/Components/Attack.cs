using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private LayerMask attackedMask;
    [SerializeField] private Weapon Weapon;
    [SerializeField] private Transform hand;

    private float attackTime;
    private Collider[] enemies = new Collider[3];
    private Health health;
    private bool CanAttack => attackTime <= 0;
    public bool IsAttacking { get; private set; }

    private void Start()
    {
        ResetTime();
        health = GetComponent<Health>();
        Instantiate(Weapon.Prefab, hand);
    }

    public bool TargetInRange(Transform target) =>
        Vector3.Distance(transform.position, target.position) < Weapon.Radius;

    public void Attacking()
    {
        if (!health.isDead)
        {
            if (CanAttack)
            {
                AnimateAttack();
                ResetTime();
                AttackNearEnemies();
            }
        }
    }
    private void Update()
    {
        attackTime -= Time.deltaTime;
        IsAttacking = Weapon.AttackCD - attackTime < 1.4f;
    }

    private void AnimateAttack()
    {
        var variation = Random.Range(0, 2);
        Animator.SetTrigger("Attack");
        Animator.SetInteger("AttackIndex", variation);
    }

    private void ResetTime() => attackTime = Weapon.AttackCD;
    private void AttackNearEnemies()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, Weapon.Radius, enemies, attackedMask);
        if (count > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    if (enemies[i].TryGetComponent<Health>(out var component))
                    {
                        component.GetDamage(Weapon.Damage);
                    }
                }
            }
        }
    }
}
