using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float XRotationSpeed = 1, YRotationSpeed = 1;
    public Transform target, player;
    float mouseX, mouseY;

    public bool disableCursor;

    public PlayerController controller;

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
        mouseX += (Input.GetAxis("Mouse X") * XRotationSpeed) * 2;
        mouseY -= Input.GetAxis("Mouse Y") * YRotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
