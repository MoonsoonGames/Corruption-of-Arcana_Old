using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    public float XRotationSpeed = 1, YRotationSpeed = 1;
    public Transform target, player;
    float mouseX, mouseY;

    public bool disableCursor;

    public PlayerController controller;

    public CinemachineVirtualCamera virtualCamRef;
    public float zoomValue = 60;
    public float zoomInterval = 0.6f;
    public float zoomMin = 50, zoomMax = 70;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = !disableCursor;
        if (disableCursor)
            Cursor.lockState = CursorLockMode.Locked;

        mouseX = transform.rotation.eulerAngles.y;
        mouseY = transform.rotation.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (controller.canMove)
        {
            CamControl();
        }
    }

    // Update is called once per frame
    void OldCamControl()
    {
        mouseX += (Input.GetAxis("Mouse X") * XRotationSpeed) * 2;
        mouseY -= Input.GetAxis("Mouse Y") * YRotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        if(Input.GetKey(KeyCode.Mouse2))
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }

    // Update is called once per frame
    void CamControl()
    {
        #region Movement

        mouseX += (Input.GetAxis("Mouse X") * XRotationSpeed) * 2;
        mouseY -= Input.GetAxis("Mouse Y") * YRotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);

        #endregion

        #region Zoom

        zoomValue -= Input.mouseScrollDelta.y * zoomInterval;
        zoomValue = Mathf.Clamp(zoomValue, zoomMin, zoomMax);
        virtualCamRef.m_Lens.FieldOfView = zoomValue;

        #endregion
    }
}