using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Change to factory implementation and add agility modifier
    [SerializeField] private float speed = 20;

    private bool canMove;
    private Vector3 _direction;


    public void Move(Vector3 direction)
    {
        _direction = direction;
        canMove = true;
    }

    private void Update()
    {
        if (canMove) transform.Translate(_direction * speed * Time.deltaTime);
    }
}