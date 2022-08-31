using System;
using System.Collections;
using UnityEngine;

public class RangedWeaponController : WeaponController
{
    public RangedWeapon rangedWeapon;

    private void Update()
    {
        var delta = Facade.timeManager.GetDeltaTime(this);
        
        if (Input.shoot && ShootTimeoutDelta <= 0.0f)
        {
            FireWeapon();

            ShootTimeoutDelta = 1 / rangedWeapon.attacksPerSecond;
            Input.shoot = false;
        }

        if (ShootTimeoutDelta >= 0.0f)
        {
            ShootTimeoutDelta -= delta;
        }
    }

    public void FireWeapon()
    {
        var position = MainCamera.transform.position + MainCamera.transform.forward;
        var direction = MainCamera.transform.forward;
        var newProjectile = Instantiate(rangedWeapon.projectile, position, Quaternion.identity);
        newProjectile.Fire(direction, Facade, rangedWeapon.Modifiers, Facade.timeManager);
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