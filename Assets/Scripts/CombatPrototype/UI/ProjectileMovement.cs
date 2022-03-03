using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetPosition;
    public float speed = 1;
    public bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = target.transform.position;
    }

    private void Update()
    {
        if (moving)
        {
            Vector3 newPosition = LerpVector(transform.position, targetPosition, Time.deltaTime * speed);

            if (transform.position == newPosition)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = newPosition;
            }
        }
    }

    //Code from COMP270 Assignment - Andrew Scott
    private Vector3 LerpVector(Vector3 a, Vector3 b, float time)
    {
        return new Vector3(Mathf.Lerp(a.x, b.x, time), Mathf.Lerp(a.y, b.y, time), Mathf.Lerp(a.z, b.z, time));
    }
}
