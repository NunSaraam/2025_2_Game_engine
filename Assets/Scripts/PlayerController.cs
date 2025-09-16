using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float runSpeed = 7f;
    private float currentSpeed;

    public float jumpPower = 5f;
    public float gravity = -9.81f;

    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;

    private CamState currentState;

    private CinemachinePOV pov;

    private CharacterController controller;
    private Vector3 velocity;

    public bool isGrounded;

    public CinemachineSwitcher cS;


    private void Start()
    {
        currentSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
            virtualCam.m_Lens.FieldOfView = 70;
        }
        else
        {
            currentSpeed = speed;
            virtualCam.m_Lens.FieldOfView = 60;

        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 camFoward = virtualCam.transform.forward;
        camFoward.y = 0;
        camFoward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();
        
        Vector3 move = (camFoward * z + camRight * x).normalized;
        if (!cS.usingFreeLook)
        controller.Move(move * currentSpeed * Time.deltaTime);

        float cameraYaw = pov.m_HorizontalAxis.Value;
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }


}
