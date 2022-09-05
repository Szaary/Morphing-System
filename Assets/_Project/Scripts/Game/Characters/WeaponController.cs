using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour, ICharacterSystem
{
    protected MovementInput Input;
    protected float ShootTimeoutDelta;
    protected Camera MainCamera;
    protected CharacterFacade Facade;
     

    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        Input = characterFacade.MovementInput;
        MainCamera = characterFacade.CameraManager.MainCamera;
        Facade.CharacterSystems.Add(this);
    }

    public abstract void Disable();

   
    
}