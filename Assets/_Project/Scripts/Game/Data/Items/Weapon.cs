using System.Collections;
using StarterAssets;
using UnityEngine;

// [CreateAssetMenu(fileName = "ITE_", menuName = "Items/RangedWeapon")]
public abstract class Weapon : Item
{
    public float attacksPerSecond = 1f;
    public float range = 1.5f;
}