using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainColor : MonoBehaviour
{
    TreeInstance[] trees;

    public Material[] mats;

    public int yourPrototypeIndex = 3;
    void Start()
    {
        Debug.Log("start");
        var terrain = GetComponent<Terrain>();
        trees = terrain.terrainData.treeInstances;

        foreach (var tree in trees)
        {
            Debug.Log("item found");
            if (tree.prototypeIndex == yourPrototypeIndex)
            {
                Debug.Log("item is bush");
                int randInt = Random.Range(0, mats.Length);

                if (GetComponentInChildren<Renderer>() != null)
                {
                    GetComponentInChildren<Renderer>().material = mats[randInt];
                }
                else
                {
                    Debug.Log("No renderer");
                }
            }
            else
            {
                Debug.Log(tree.prototypeIndex);
            }
        }
    }

}
