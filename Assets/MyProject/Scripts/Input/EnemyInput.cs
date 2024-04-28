using UnityEngine;

namespace Assets.MyProject.Scripts
{
    internal class EnemyInput : InputManager
    {
        public override bool AttackPressed { get; protected set; }
        public override Vector3 Motion { get; protected set; }

        private void Update()
        {
            AttackPressed = true;

            Motion = Vector3.zero;
        }
    }
}
