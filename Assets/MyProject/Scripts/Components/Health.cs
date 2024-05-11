using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] public float currentHealth;
    [SerializeField] private float timeAfterDead = 5;

    [NonSerialized] public bool isDead;

    public void Init(float maxHealth) => currentHealth = maxHealth;
    
    private void Update()
    {
        if (isDead)
            timeAfterDead -= Time.deltaTime;
        if (timeAfterDead < 0) 
            Destroy(gameObject);
    }
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        Animator.SetTrigger("IsAttackedBy");

        if (currentHealth <= 0) Die();
    }
    private void Die()
    {
        isDead = true;
        Animator.SetTrigger("Death");
    }
}
