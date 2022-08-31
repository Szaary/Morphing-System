using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    protected FpsInput Input;
    protected float ShootTimeoutDelta;
    protected Camera MainCamera;
    protected CharacterFacade Facade;
     

    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        Input = characterFacade.starterInputs;
        MainCamera = characterFacade.cameraManager.MainCamera;
    }
}