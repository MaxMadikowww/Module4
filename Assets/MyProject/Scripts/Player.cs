using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Movement mover;
    [SerializeField] private Attack attacker;

    [SerializeField] private InputMeneger input;

    private void Start()
    {
        var role = StaticData.Role;
        health.Init(role.Health);
        attacker.Init(role.Weapon);
    }
    private void Update()
    {
        if (input.AttackPressed)
        {
            attacker.Attacking();
        }
        if (!attacker.IsAttacking)
        {
            mover.Move(input.Motion);
        }
    }
}
