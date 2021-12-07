using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMat : MonoBehaviour
{
    public Material[] mats;

    // Start is called before the first frame update
    void Awake()
    {
        int randInt = Random.Range(0, mats.Length);

        if (GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().material = mats[randInt];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
