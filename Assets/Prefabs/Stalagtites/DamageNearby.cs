using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNearby : MonoBehaviour
{

    public void DealDamage(int damage, float range)
    {
        PlayerController controller = GameObject.FindObjectOfType<PlayerController>();

        if (controller != null)
        {
            float distance = Vector3.Distance(transform.position, controller.gameObject.transform.position);
            //Debug.Log("Distance: " + distance);
            if (distance < range)
            {
                Debug.Log("Deal damage");
                controller.DealDamage(damage);
            }
        }

        Invoke("DestroySelf", 0.2f);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}