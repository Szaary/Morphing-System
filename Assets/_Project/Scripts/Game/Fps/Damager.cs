using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage = 2f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
        {
            return;
        }
        if (other.transform.parent.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(damage, transform.position);
        }
    }
}