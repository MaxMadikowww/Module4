using UnityEngine;

namespace Assets.MyProject.Scripts
{
    public abstract class InputManager : MonoBehaviour
    {
        public abstract bool AttackPressed { get; protected set; }
        public abstract Vector3 Motion { get; protected set; }
    }
}
