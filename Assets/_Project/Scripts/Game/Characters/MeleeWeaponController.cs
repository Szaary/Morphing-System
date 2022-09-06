using System;
using UnityEngine;

public class MeleeWeaponController : WeaponController
{
    public MeleeWeapon weapon; 
    
    
    /*
    private void Update()
    {
        var delta = Facade.TimeManager.GetDeltaTime(this);
        
        if (Input.melee && AttackTimeout <= 0.0f)
        {
            Attack();

           // AttackTimeout = 1 / meleeWeapon.;
            Input.melee = false;
        }

        if (AttackTimeout >= 0.0f)
        {
            AttackTimeout -= delta;
        }
    }
    */
    
    

    public void Attack()
    {
        if (Physics.SphereCast(transform.position, weapon.range, transform.forward, out var hit, 100))
        {
            if (hit.transform.parent.TryGetComponent(out Damageable damageable))
            {
                Debug.Log("Target hit");
                damageable.TakeDamage(Facade, weapon.Modifiers);
            }
        }
    }
    
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.green;
        Gizmos.DrawWireSphere(transform.position, weapon.range);
    }
#endif

    public override void Disable()
    {
        enabled = false;
    }

    public override void PlaySfx()
    {
        soundEmitter.Play(weapon.eventReference);
    }
}