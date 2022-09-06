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
    
    public float TimeBetweenAttacks => weapon.timeBetweenAttacks;
    public float Range => weapon.range;
    
    private void Start()
    {
        Magazine = weapon.MagazineSize;
    }

    private void Update()
    {
        if (weapon == null) return;
        var delta = Facade.TimeManager.GetDeltaTime(this);
        if (Input.shoot && AttackTimeout >= TimeBetweenAttacks && Magazine > 0)
        {
            AttackTimeout = 0;
            
            FireWeapon();
            magazineChanged?.Invoke(Magazine, weapon.MagazineSize);
            source.GenerateImpulse();
            if (Magazine == 0)
            {
                StartCoroutine(ReloadWeapon());
            }
        }

        if (AttackTimeout < TimeBetweenAttacks)
        {
            AttackTimeout += delta;
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
        Magazine--;
        var position = MainCamera.transform.position + MainCamera.transform.forward;
        var direction = MainCamera.transform.forward;
        var newProjectile = Instantiate(weapon.projectile, position, Quaternion.identity);
        newProjectile.Fire(direction, Facade, weapon.Modifiers, Facade.TimeManager);
        newProjectile.StartCoroutine(DestroyAfterTime(newProjectile));
    }
    
    public void FireWeaponForward()
    {
        var newProjectile = Instantiate(weapon.projectile, transform.position, Quaternion.identity);
        newProjectile.Fire(transform.forward, Facade, weapon.Modifiers, Facade.TimeManager);
        newProjectile.StartCoroutine(DestroyAfterTime(newProjectile));
    }

    private bool attacked;
    
    // USED BY GRAPH 
    public void FireWeaponWithCooldown()
    {
        if (!attacked)
        {
            attacked = true;

            var newProjectile = Instantiate(weapon.projectile, transform.position, Quaternion.identity);
            newProjectile.Fire(transform.forward, Facade, weapon.Modifiers, Facade.TimeManager);
            newProjectile.StartCoroutine(DestroyAfterTime(newProjectile));
            Invoke(nameof(Reset), TimeBetweenAttacks);
        }
    }
    
    public void Reset()
    {
        attacked = false;
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