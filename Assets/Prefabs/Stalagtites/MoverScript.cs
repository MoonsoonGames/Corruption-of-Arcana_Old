using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour
{
    public Vector3 movement;

    private void FixedUpdate()
    {
        gameObject.transform.position += movement;
    }
}
