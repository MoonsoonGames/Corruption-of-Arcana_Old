using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalagtiteSpawner : MonoBehaviour
{
    public Object spawnObject;
    public int radius;
    public int yVariation;

    float currentTime = 0;
    float spawnRate;
    public Vector2 spawnRateRange;

    private void Start()
    {
        spawnRate = Random.Range(spawnRateRange.x, spawnRateRange.y);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnRate)
        {
            SpawnStalagtite();

            spawnRate = Random.Range(spawnRateRange.x, spawnRateRange.y);
            currentTime = 0;
        }
    }

    void SpawnStalagtite()
    {
        Vector3 spawnPos = this.transform.position;
        spawnPos.x += Random.Range(-radius, radius);
        spawnPos.y += Random.Range(-yVariation, yVariation);
        spawnPos.z += Random.Range(-radius, radius);

        Instantiate(spawnObject, spawnPos, this.transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 size = new Vector3(radius * 2, 1, radius * 2);

        Gizmos.DrawWireCube(transform.position, size);
    }
}
