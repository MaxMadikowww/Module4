using Assets.MyProject.Scripts;
using UnityEngine;

public class PlayerInput : InputManager
{
    public override bool AttackPressed {  get; protected set; }
    public override Vector3 Motion {  get; protected set; }

    private void Update()
    {
        AttackPressed = Input.GetMouseButtonDown(0);

        Motion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
