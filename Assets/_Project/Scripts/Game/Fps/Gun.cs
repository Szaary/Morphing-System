using System.Collections;
using StarterAssets;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float fireCooldown = 0.2f;
    
    
    private StarterAssetsInputs _input;
    private float _shootTimeoutDelta;
    private Camera _mainCamera;


    public void Initialize(CharacterFacade characterFacade)
    {
        _input = characterFacade.starterInputs;
        _mainCamera = characterFacade.cameraManager.MainCamera;
    }
   
    
    private void Update()
    {
        #if UNITY_EDITOR
        if (_mainCamera != null)
        {
            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.green);
        }
#endif
        if (_input.shoot  && _shootTimeoutDelta <= 0.0f)
        {
            var position = _mainCamera.transform.position+_mainCamera.transform.forward;
            var direction = _mainCamera.transform.forward;
            
            Shoot(position, direction);
            _shootTimeoutDelta = fireCooldown;
            _input.shoot = false;
        }
        if (_shootTimeoutDelta >= 0.0f)
        {
            _shootTimeoutDelta -= Time.deltaTime;
        }
    }

    private void Shoot(Vector3 position, Vector3 direction)
    {
        var newProjectile = Instantiate(projectile, position, Quaternion.identity);
        newProjectile.Move(direction);
        
        newProjectile.StartCoroutine(DestroyAfterTime(newProjectile));
    }
    
    
    private IEnumerator DestroyAfterTime(Projectile projectile)
    {
        yield return new WaitForSeconds(3);
        Destroy(projectile);
    }

  
}
