using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float RotationSpeed = 10;
    public Transform target, player;
    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        CamControl();
    }

    // Update is called once per frame
    void CamControl()
    {
        mouseX += (Input.GetAxis("Mouse X") * RotationSpeed) * 2;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        if(Input.GetKey(KeyCode.CapsLock))
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }
}
