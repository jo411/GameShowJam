using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof( MeshFilter))]
public class PlaneGenerator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] verticies;
    int[] triangles;
    Vector2[] uv;
    public Material mat;


    public int width;
    int height;
    public float CellSize;

    private void Start()
    {       
        CreateShape();
        UpdateMesh();
    }

    private void OnValidate()
    {
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        if(CellSize<0)
        {
            CellSize = 0;
        }
        if(width<=0)
        {
            width = 1;
        }

        height = width;

        if(mesh==null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        verticies = new Vector3[(width + 1) * (height + 1)];
        uv = (mesh.uv.Length == verticies.Length) ? mesh.uv : new Vector2[verticies.Length];
        int i = 0;
        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                Vector2 percent = new Vector2(x, z) / (width*height - 1);
                verticies[i] = new Vector3(x*CellSize, 0, z*CellSize);

                i++;
            }
        }


        triangles = new int[width*height*6];
        int tris = 0;
        int vert = 0;

        for (int z = 0; z <height; z++)
        {
            for (int x = 0; x <width; x++)
            {               
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + width + 1;
                triangles[tris + 2] = vert + 1;

                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + width + 1;
                triangles[tris + 5] = vert + width + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        


      


    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();        
        GetComponent<MeshRenderer>().material = mat;
    }

    private void OnDrawGizmos()
    {
        //if(verticies== null) { return; }
        //for (int i = 0; i < verticies.Length; i++)
        //{
        //    Gizmos.DrawSphere(verticies[i]+transform.position, .1f);
        //}
    }
}
