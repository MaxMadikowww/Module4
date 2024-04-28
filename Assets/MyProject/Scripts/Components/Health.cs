using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float timeAfterDead = 5;

    [NonSerialized] public bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
    }
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
        if (currentHealth < 0) Die();
    }
    private void Die()
    {
        isDead = true;
        Animator.SetTrigger("Death");
    }
}
