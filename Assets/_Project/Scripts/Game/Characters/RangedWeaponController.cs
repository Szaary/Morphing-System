using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class RangedWeaponController : WeaponController, ICharacterSystem
{
    public event Action<int, int> magazineChanged;
    public RangedWeapon weapon;
    [SerializeField] private CinemachineImpulseSource source; 
    public int Magazine { get; private set; }
    
    public float TimeBetweenAttacks => weapon.attacksPerSecond;
    public float Range => weapon.range;
    
    private void Start()
    {
        Magazine = weapon.MagazineSize;
    }

    private void Update()
    {
        if (weapon == null) return;
        var delta = Facade.TimeManager.GetDeltaTime(this);

        if (Input.shoot && ShootTimeoutDelta <= 0.0f && Magazine > 0)
        {
            FireWeapon();
            ShootTimeoutDelta = 1 / TimeBetweenAttacks;

            Magazine--;
            magazineChanged?.Invoke(Magazine, weapon.MagazineSize);
            source.GenerateImpulse();
            if (Magazine == 0)
            {
                StartCoroutine(ReloadWeapon());
            }
        }

        if (ShootTimeoutDelta >= 0.0f)
        {
            ShootTimeoutDelta -= delta;
        }
    }

    private IEnumerator ReloadWeapon()
    {
        yield return new WaitForSeconds(weapon.reloadTime);
        Magazine = weapon.MagazineSize;
        magazineChanged?.Invoke(Magazine, weapon.MagazineSize);
    }


    public void FireWeapon()
    {
        var position = MainCamera.transform.position + MainCamera.transform.forward;
        var direction = MainCamera.transform.forward;
        var newProjectile = Instantiate(weapon.projectile, position, Quaternion.identity);
        newProjectile.Fire(direction, Facade, weapon.Modifiers, Facade.TimeManager);
        newProjectile.StartCoroutine(DestroyAfterTime(newProjectile));
    }
    
    public void FireWeapon(Vector3 shootDirection)
    {
        var newProjectile = Instantiate(weapon.projectile, transform.position, Quaternion.identity);
        newProjectile.Fire(shootDirection, Facade, weapon.Modifiers, Facade.TimeManager);
        newProjectile.StartCoroutine(DestroyAfterTime(newProjectile));
    }
    

    private IEnumerator DestroyAfterTime(Projectile projectile)
    {
        yield return new WaitForSeconds(3);
        Destroy(projectile);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (weapon == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * weapon.range);
    }
#endif
    public override void Disable()
    {
        enabled = false;
    }
}