using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosion : MonoBehaviour
{
    public GameObject parent;
    public Object explosion;
    public int damage;
    public float range;

    private void OnTriggerEnter(Collider other)
    {
        GameObject explosionRef = Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject;
        DamageNearby damageScript = explosionRef.GetComponent<DamageNearby>();
        damageScript.DealDamage(damage, range);
        
        Destroy(parent);
    }
}
