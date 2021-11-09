using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float destroyDelay;

    private void Start()
    {
        Invoke("DestroySelf", destroyDelay);
    }


    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
