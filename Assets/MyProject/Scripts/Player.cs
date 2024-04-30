using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private PlayerMovement mover;
    [SerializeField] private Attack attacker;

    [SerializeField] private InputMeneger input;

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
