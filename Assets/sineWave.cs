using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sineWave : MonoBehaviour
{

    Mesh mesh;

    Vector3[] verts;

    MeshCollider meshCol;

    public float SinValue;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        verts = mesh.vertices;

        meshCol = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i].y = Mathf.Sin(SinValue * i + Time.time);
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        meshCol.sharedMesh = mesh;
    }
}
