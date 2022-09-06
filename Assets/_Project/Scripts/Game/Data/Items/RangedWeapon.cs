using UnityEngine;

[CreateAssetMenu(fileName = "ITE_", menuName = "Items/RangedWeapon")]
public class RangedWeapon : Weapon
{
    public Projectile projectile;
    [SerializeField] private int magazineSize=30;
    public float reloadTime=2;
    public int MagazineSize => magazineSize;
}
