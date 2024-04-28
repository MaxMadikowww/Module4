using Assets.MyProject.Scripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController control;
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private Animator Animator;
    [SerializeField] private InputManager inputM;

    private Health health;

    private void Start() => health = GetComponent<Health>();

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        if (!health.isDead)
        {
            if (inputM.Motion.sqrMagnitude > 0.05f)
            {
                var direction = transform.TransformDirection(inputM.Motion);
                direction.Normalize();
                direction += Physics.gravity;

                control.Move(direction * Speed * Time.deltaTime);
                Animator.SetFloat("Movement", control.velocity.magnitude);
            }
            else
            {
                Animator.SetFloat("Movement", 0);
            }
        }
    }
    private void Rotate()
    {
        if (!health.isDead)
        {
            var mouseX = Input.GetAxis("Mouse X") * Sensitivity;

            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
