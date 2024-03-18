using UnityEngine;
using System.Collections.Generic;


public class CameraResolution : MonoBehaviour {
    public float targetAspect = 16f / 9f; // Set your desired aspect ratio here

    private void Start()
    {
        Camera mainCamera = Camera.main;

        // Calculate the current aspect ratio
        float currentAspect = (float)Screen.width / Screen.height;

        // Calculate the desired FOV to maintain the aspect ratio
        float targetFOV = mainCamera.fieldOfView * (targetAspect / currentAspect);

        // Apply the calculated FOV to the camera
        mainCamera.fieldOfView = targetFOV;
    }
}
