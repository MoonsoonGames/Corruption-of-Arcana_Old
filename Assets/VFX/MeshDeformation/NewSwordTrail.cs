using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class NewSwordTrail : MonoBehaviour
{
    #region Mesh Generation

    [Header("Transforms")]
    public Transform parentTransform;
    public GameObject player;
    public GameObject swordBase;
    public GameObject swordPoint;
    public GameObject trailPoint;
    public GameObject desiredTrailPoint;
    public float lerpSpeed = 0.5f;

    private Vector3 offSet = new Vector3(17.6f, 14.0f, 23.8f);

    [Header("Mesh Generation")]
    [Range(3, 10)]
    public int vertexCount; //Needs to be an odd number greater than or equal to 3

    Mesh mesh;

    Vector3[] verts;
    int[] tris;

    MeshFilter meshFilter;
    MeshCollider meshCollider;

    #endregion

    #region Material

    [Header("Material")]
    public Gradient gradient;

    Vector2[] uvs;
    public float uvScale = 1f;

    Color[] colours;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        if (meshFilter != null)
        {
            meshFilter.mesh = mesh;
        }

        if (meshCollider != null)
        {
            meshCollider.sharedMesh = mesh;
        }

        CreateQuad();
        UpdateMesh();

        //ResetTransform();
    }

    private void Update()
    {
        CreateQuad();
        UpdateMesh();
        //ResetTransform();
        //parentTransform.position = new Vector3();
    }

    void CreateQuad()
    {
        #region Trail Point

        Vector3 newPos = new Vector3();

        newPos = LinearLerp(trailPoint.transform.position, desiredTrailPoint.transform.position, Time.deltaTime * lerpSpeed);

        //Debug.Log(trailPoint.transform.position + " || " + desiredTrailPoint.transform.position);

        trailPoint.transform.position = newPos;

        #endregion

        #region Vertices

        if (verts != null)
        {
            verts = new Vector3[4];

            verts[2] = verts[0];
            verts[3] = verts[2];

            verts[0] = swordBase.transform.position;
            verts[1] = swordPoint.transform.position;
        }
        else
        {
            verts = new Vector3[4];

            verts[0] = swordBase.transform.position;
            verts[1] = swordPoint.transform.position;

            verts[2] = swordBase.transform.position;
            verts[3] = swordPoint.transform.position;
        }

        #endregion

        #region Triangles

        tris = new int[vertexCount * 3];

        for (int t = 0, v = 0; t < vertexCount + 3; t+=3, v++)
        {
            tris[t] = v;
            tris[t + 1] = v + 1;
            tris[t + 2] = vertexCount - 1;
        }

        #endregion

        #region Material
        
        uvs = new Vector2[verts.Length];

        for (int i = 0; i <= vertexCount; i++)
        {
            //Issue with even numbers
            uvs[i] = new Vector2((float)i, uvScale);
            i++;
        }
        #endregion
    }

    void UpdateMesh()
    {
        if (mesh != null)
        {
            mesh.Clear();

            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            mesh.uv = uvs;

            colours = new Color[verts.Length];

            for (int i = 0; i < vertexCount; i++)
            {
                float distance = Vector3.Magnitude(swordPoint.transform.position - verts[i]);
                distance = Remap(distance, 0, 2.5f, 0, 1);

                colours[i].a = Mathf.Clamp(gradient.Evaluate(distance).a, 0, 1);
            }

            mesh.colors = colours;

            //Debug.Log(colours[2].a);
        }
    }

    #region Helper functions

    private void OnDrawGizmosSelected()
    {
        if (verts != null)
        {
            for (int i = 0; i < verts.Length; i++)
            {
                Gizmos.DrawSphere(verts[i], 0.1f);
            }
        }
    }

    private void ResetTransform()
    {
        transform.position = new Vector3(17.6f, 14.0f, 23.8f);
        transform.eulerAngles = new Vector3(0, 0, 180);
        transform.localScale = new Vector3(0, 0, 0);
    }

    private float Remap(float value, float inMin, float inMax, float outMin, float outMax)
    {
        //https://docs.unity3d.com/Packages/com.unity.shadergraph@6.9/manual/Remap-Node.html
        return outMin + (value - inMin) * (outMax - outMin) / (inMax - inMin);
    }

    private Vector3 LinearLerp(Vector3 v0, Vector3 v1, float lerpPoint)
    {
        return new Vector3(Mathf.Lerp(v0.x, v1.x, lerpPoint), Mathf.Lerp(v0.y, v1.y, lerpPoint), Mathf.Lerp(v0.z, v1.z, lerpPoint));
    }

    private Vector3 QuadraticLerp(Vector3 v0, Vector3 v1, Vector3 v2, float lerpPoint)
    {
        Vector3 l0Point = LinearLerp(v0, v1, lerpPoint);
        Vector3 l1Point = LinearLerp(v1, v2, lerpPoint);

        Vector3 q0 = LinearLerp(l0Point, l1Point, lerpPoint);

        return q0;
    }

    #endregion
}
