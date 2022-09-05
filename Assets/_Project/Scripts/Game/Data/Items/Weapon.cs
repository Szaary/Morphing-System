using System.Collections;
using StarterAssets;
using UnityEngine;

// [CreateAssetMenu(fileName = "ITE_", menuName = "Items/RangedWeapon")]
public abstract class Weapon : Item
{
    public float timeBetweenAttacks = 0.5f;
    public float range = 1.5f;
}