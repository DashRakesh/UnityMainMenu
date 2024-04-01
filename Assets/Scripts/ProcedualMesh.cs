using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualMesh : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private MeshFilter filter;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();

        Vector3 p0 = Vector3.zero;
        Vector3 p1 = Vector3.right;
        Vector3 p2 = Vector3.up;
        Vector3 p3 = Vector3.up + Vector3.right;

        vertices.Add(p0);
        vertices.Add(p1);
        vertices.Add(p2);

        List<int> tringle = new List<int>();

        tringle.Add(0);
        tringle.Add(1);
        tringle.Add(3);

        tringle.Add(0);
        tringle.Add(3);
        tringle.Add(2);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = tringle.ToArray();
        filter.mesh = mesh;
    }
}
