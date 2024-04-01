using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualCircle : MonoBehaviour
{
    [Header(" Element ")]
    [SerializeField] private MeshFilter filter;

    [Header("Setting")]
    [Range(3, 20)]
    [SerializeField] private int circlResolution;
    [SerializeField] private int radius;

    [Header("Data")]
    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<int> tringles = new List<int>(); 

    // Start is called before the first frame update
    void Start()
    {
        GenerateCircle();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateCircle()
    {
        Mesh mesh = new Mesh();

        vertices.Clear();
        tringle.Clear();

        List<Vector3> vertices = new List<Vector3>();
        List<int> tringles = new List<int>();

        Vector3 center = Vector3.zero;
        vertices.Add(center);

        // Angles between two points
        float anglebetweenpoints = 360f / circlResolution;

        for (int i= 0; i < circlResolution; i++)
        {
            Vector3 vertex = center;

            vertex.x += radius * Mathf.Cos(anglebetweenpoints * i * Mathf.Deg2Rad);
            vertex.y += radius * Mathf.Sin(anglebetweenpoints * i * Mathf.Deg2Rad);

            vertices.Add(vertex);

        }


        mesh.vertices = vertices.ToArray();
        mesh.triangles = tringles.ToArray();

        filter.mesh = mesh;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(filter.mesh == null)
        {
            return;
        }
        for(int i =0; i < filter.mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(filter.mesh.vertices[i], .1f);
        }
    }
}
