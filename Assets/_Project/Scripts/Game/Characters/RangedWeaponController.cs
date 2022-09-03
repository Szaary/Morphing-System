using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class RangedWeaponController : WeaponController
{
    public event Action<int, int> magazineChanged;
    public RangedWeapon rangedWeapon;
    [SerializeField] private CinemachineImpulseSource source; 
    public int Magazine { get; private set; }

    private void Start()
    {
        Magazine = rangedWeapon.MagazineSize;
    }

    private void Update()
    {
        if (rangedWeapon == null) return;
        var delta = Facade.TimeManager.GetDeltaTime(this);

        if (Input.shoot && ShootTimeoutDelta <= 0.0f && Magazine > 0)
        {
            FireWeapon();
            ShootTimeoutDelta = 1 / rangedWeapon.attacksPerSecond;

            Magazine--;
            magazineChanged?.Invoke(Magazine, rangedWeapon.MagazineSize);
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
        yield return new WaitForSeconds(rangedWeapon.reloadTime);
        Magazine = rangedWeapon.MagazineSize;
        magazineChanged?.Invoke(Magazine, rangedWeapon.MagazineSize);
    }


    public void FireWeapon()
    {
        var position = MainCamera.transform.position + MainCamera.transform.forward;
        var direction = MainCamera.transform.forward;
        var newProjectile = Instantiate(rangedWeapon.projectile, position, Quaternion.identity);
        newProjectile.Fire(direction, Facade, rangedWeapon.Modifiers, Facade.TimeManager);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * rangedWeapon.range);
    }
#endif
}