using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamState
{
    TPS,
    FreeLook
}

public class CinemachineSwitcher : MonoBehaviour
{
    public CamState currentState = CamState.TPS;
    public CinemachineVirtualCamera virtualCam;
    public CinemachineFreeLook freeLookCam;

    public bool usingFreeLook = false;

    private void Start()
    {
        virtualCam.Priority = 10;
        freeLookCam.Priority = 0;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook && currentState == CamState.TPS)
            {
                currentState = CamState.FreeLook;
                freeLookCam.Priority = 20;
                virtualCam.Priority = 0;
                Debug.Log("FreeLook 모드");
            }
            else if(!usingFreeLook && currentState == CamState.FreeLook)
            {
                currentState = CamState.TPS;
                virtualCam.Priority = 20;
                freeLookCam.Priority = 0;
                Debug.Log("Virtual 모드");
            }

        }
    }
}
