using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Configs/Weapon", order = 0)]

public class Weapon : ScriptableObject
{
    public float AttackCD;
    public float Damage;
    public float Radius;
    public GameObject Prefab;
}
