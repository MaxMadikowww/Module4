using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private LayerMask attackedMask;
    [SerializeField] private Transform hand;
    private Weapon weapon;

    private float attackTime = 0;
    private Collider[] enemies = new Collider[3];
    private Health health;
    private bool CanAttack => attackTime <= 0;
    public bool IsAttacking { get; private set; }

    public void Init(Weapon weapon)
    {
        this.weapon = weapon;

        health = GetComponent<Health>();
        Instantiate(this.weapon.Prefab, hand);

        if (this.weapon.OverrideController != null)
            Animator.runtimeAnimatorController = this.weapon.OverrideController;
    }

    public bool TargetInRange(Transform target) =>
        Vector3.Distance(transform.position, target.position) < weapon.Radius;

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
        IsAttacking = weapon.AttackCD - attackTime < 1.4f;
    }

    private void AnimateAttack()
    {
        var variation = Random.Range(0, 2);
        Animator.SetTrigger("Attack");
        Animator.SetInteger("AttackIndex", variation);
    }

    private void ResetTime() => attackTime = weapon.AttackCD;
    private void AttackNearEnemies()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, weapon.Radius, enemies, attackedMask);
        if (count > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    if (enemies[i].TryGetComponent<Health>(out var component))
                    {
                        component.GetDamage(weapon.Damage);
                    }
                }
            }
        }
    }
}
