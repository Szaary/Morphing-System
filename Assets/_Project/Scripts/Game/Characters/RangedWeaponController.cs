using System.Collections;
using UnityEngine;

public class RangedWeaponController : WeaponController
{
    public RangedWeapon rangedWeapon;

    private void Update()
    {
#if UNITY_EDITOR
        if (MainCamera != null)
        {
            Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.forward, Color.green,
                rangedWeapon.distance);
        }
#endif

        if (Input.shoot && ShootTimeoutDelta <= 0.0f)
        {
            FireWeapon();

            ShootTimeoutDelta = 1 / rangedWeapon.attacksPerSecond;
            Input.shoot = false;
        }

        if (ShootTimeoutDelta >= 0.0f)
        {
            ShootTimeoutDelta -= Time.deltaTime;
        }
    }

    public void FireWeapon()
    {
        var position = MainCamera.transform.position + MainCamera.transform.forward;
        var direction = MainCamera.transform.forward;
        var newProjectile = Instantiate(rangedWeapon.projectile, position, Quaternion.identity);
        newProjectile.Fire(direction, Facade, rangedWeapon.Modifiers);
        newProjectile.StartCoroutine(DestroyAfterTime(newProjectile));
    }


    private IEnumerator DestroyAfterTime(Projectile projectile)
    {
        yield return new WaitForSeconds(3);
        Destroy(projectile);
    }
}