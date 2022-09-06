using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraManager : MonoBehaviour
{
    private const int MaxCameraPriority = 30;
    private const int UsualCameraPriority = 15;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;
    
    [SerializeField] private List<Settings> cameras;

    [SerializeField] private CinemachineVirtualCameraBase fpsCamera;
    [SerializeField] private CinemachineVirtualCameraBase turnBasedCamera;
    [SerializeField] private CinemachineVirtualCameraBase platformCamera;
    [SerializeField] private CinemachineVirtualCameraBase fppCamera;

  
    public Camera MainCamera => mainCamera;
    private GameManager _gameManager;
    private CharactersLibrary _library;


    [Inject]
    public void Construct(GameManager gameManager, CharactersLibrary library)
    {
        _gameManager = gameManager;
        _library = library;
    }

    private void Start()
    {
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;


        if (_library.GetControlledCharacter(out var facade) == Result.Success)
        {
            OnControlledCharacterChanged(facade);
        }
        _library.ControlledCharacterChanged += OnControlledCharacterChanged;
    }

    private void OnControlledCharacterChanged(CharacterFacade player)
    {
        platformCamera.Follow = player.transform;
        platformCamera.LookAt = player.transform;

        fppCamera.Follow = player.transform;
        fppCamera.LookAt = player.transform;
    }

    private void OnGameModeChanged(GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            SetTurnBasedCamera();
        }
        else if (newMode == GameMode.Fps)
        {
            SetFpsCamera();
        }
        else if (newMode == GameMode.Adventure)
        {
            SetFollowLookCamera(platformCamera);
        }
        else if (newMode == GameMode.Tpp)
        {
            SetFollowLookCamera(fppCamera);
        }
    }



    private void OnDestroy()
    {
        _gameManager.GameModeChanged -= OnGameModeChanged;
        _library.ControlledCharacterChanged -= OnControlledCharacterChanged;
    }

    public void SetFpsCamera()
    {
        var result = _library.GetControlledCharacter(out CharacterFacade facade);
        if (result != Result.Success)
        {
            Debug.Log(typeof(CameraManager) + " " + result);
            return;
        }

        var cameraFpsFollowPoint = facade.movement.cameraFpsFollowPoint;

        foreach (var cam in cameras)
        {
            if (cam.camera == fpsCamera)
            {
                cam.camera.Priority = MaxCameraPriority;
                SetupCameraAfterTarget(cam.camera, cameraFpsFollowPoint);
                if (_library.SelectRandomEnemy(facade.Alignment) != null)
                {
                    facade.LookAt(_library.SelectRandomEnemy(facade.Alignment).transform);
                }
            }
            else
            {
                cam.camera.Priority = UsualCameraPriority;
            }
        }
    }
    public void SetFollowLookCamera(CinemachineVirtualCameraBase cameraToSet)
    {
        var result = _library.GetControlledCharacter(out CharacterFacade facade);
        if (result != Result.Success)
        {
            Debug.Log(typeof(CameraManager) + " " + result);
            return;
        }
        var cameraFpsFollowPoint = facade.movement.cameraFpsFollowPoint;

        foreach (var cam in cameras)
        {
            if (cam.camera == cameraToSet)
            {
                cam.camera.Priority = MaxCameraPriority;
                cam.camera.LookAt = cameraFpsFollowPoint;
                cam.camera.Follow = cameraFpsFollowPoint;
            }
            else
            {
                cam.camera.Priority = UsualCameraPriority;
            }
        }
    }


    public void SetTurnBasedCamera()
    {
        foreach (var cam in cameras)
        {
            if (cam.camera == turnBasedCamera)
            {
                cam.camera.Priority = MaxCameraPriority;
            }
            else
            {
                cam.camera.Priority = UsualCameraPriority;
            }
        }
    }


    private void SetupCameraAfterTarget(CinemachineVirtualCameraBase cam, Transform cameraTarget)
    {
        cam.Follow = cameraTarget;
    }

    [Serializable]
    public struct Settings
    {
        public CinemachineVirtualCameraBase camera;
    }
}