using System.Collections.Generic;
using FMODUnity;
using StarterAssets;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour, ICharacterSystem
{
    [SerializeField] protected StudioEventEmitter soundEmitter;
    
    protected MovementInput Input;
    protected float AttackTimeout;
    protected Camera MainCamera;
    protected CharacterFacade Facade;
    protected ProjectileMemoryPool Pool;

    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        Pool = characterFacade.ProjectilePool;
        Input = characterFacade.MovementInput;
        MainCamera = characterFacade.CameraManager.MainCamera;
        SubscribeToCharacterSystems();
    }
    public void SubscribeToCharacterSystems()
    {
        Facade.CharacterSystems.Add(this);
    }

    public abstract void Disable();
    public abstract void PlaySfx();
}