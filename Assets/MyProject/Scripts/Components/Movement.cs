using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController control;
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private Animator Animator;

    private Health health;

    private void Start() => health = GetComponent<Health>();

    private void Update() => Rotate();

    public void Move(Vector3 input)
    {
        if (!health.isDead)
        {
            if (input.sqrMagnitude > 0.05f)
            {
                MoveTo(input);
            }
            else
            {
                SetAnimatorSpeed(0);
            }
        }
    }

    private void MoveTo(Vector3 input)
    {
        var direction = transform.TransformDirection(input);
        direction.Normalize();
        direction += Physics.gravity;

        control.Move(direction * Speed * Time.deltaTime);
        SetAnimatorSpeed(control.velocity.magnitude);
    }

    private void Rotate()
    {
        if (!health.isDead)
        {
            var mouseX = Input.GetAxis("Mouse X") * Sensitivity;

            transform.Rotate(Vector3.up * mouseX);
        }
    }
    private void SetAnimatorSpeed(float speed)
    {
        Animator.SetFloat("Movement", speed);
    }
}
