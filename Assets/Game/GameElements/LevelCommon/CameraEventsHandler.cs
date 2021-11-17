using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventsHandler : MonoBehaviour
{
    public CinemachineVirtualCamera camera;

    public PlayerMountedEvent playerMountedEvent;

    private void Start()
    {
        playerMountedEvent.onPlayerMounted = OnPlayerMounted;
    }

    private void OnPlayerMounted(GameObject vehicle)
    {
        // camera follows vehicle
        camera.Follow = vehicle.transform;
    }
}
