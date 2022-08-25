using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private const int MaxCameraPriority = 30; 
    private const int UsualCameraPriority = 15; 

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;

    [SerializeField] private List<Settings> cameras;

    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    [SerializeField] private CinemachineVirtualCamera turnBasedCamera;
    
    public void SetFpsCamera(Transform cameraTarget)
    {
        foreach (var cam in cameras)
        {
            if (cam.camera == fpsCamera)
            {
                cam.camera.Priority = MaxCameraPriority;
                SetupCameraAfterTarget(cam.camera, cameraTarget);
            }
            else
            {
                cam.camera.Priority = MaxCameraPriority;
            }
        }
    }
    
    private void SetupCameraAfterTarget(CinemachineVirtualCamera cam, Transform cameraTarget)
    {
        cam.Follow = cameraTarget;
    }

    [Serializable]
    public struct Settings
    {
        public CinemachineVirtualCamera camera;
    }
}