using UnityEngine;

public class InputMeneger : MonoBehaviour
{
    public bool AttackPressed {  get; private set; }
    public Vector3 Motion {  get; private set; }

    private void Update()
    {
        AttackPressed = Input.GetMouseButtonDown(0);

        Motion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
