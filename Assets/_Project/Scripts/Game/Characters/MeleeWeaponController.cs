using System;
using UnityEngine;

public class MeleeWeaponController : WeaponController
{
    public MeleeWeapon meleeWeapon; 
    
    
    private void Update()
    {
        var delta = Time.deltaTime;
        
        if (Input.melee && ShootTimeoutDelta <= 0.0f)
        {
            SwingWeapon();

            ShootTimeoutDelta = 1 / meleeWeapon.attacksPerSecond;
            Input.melee = false;
        }

        if (ShootTimeoutDelta >= 0.0f)
        {
            ShootTimeoutDelta -= delta;
        }
    }

    public void SwingWeapon()
    {
        if (Physics.SphereCast(transform.position, meleeWeapon.range, transform.forward, out var hit, 100))
        {
            if (hit.transform.parent.TryGetComponent(out Damageable damageable))
            {
                Debug.Log("Target hit");
                damageable.TakeDamage(Facade, meleeWeapon.Modifiers);
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeWeapon.range);
    }
#endif

}