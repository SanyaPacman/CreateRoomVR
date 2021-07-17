using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)),RequireComponent(typeof(MeshRenderer)),RequireComponent(typeof(MeshCollider))]
public class ProceduralRoomGeneration : MonoBehaviour
{    
    [Range(0,50f)]
    public float sizeY;
    [Range(0, 50f)]
    public float sizeX;
    [Range(0, 50f)]
    public float sizeZ;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    public void GenerateMesh()
    {
        GenerateVertices();
        GenerateTriangles();
        SetData();
    }
    private void GenerateVertices()
    {
        vertices = new Vector3[8];
        uvs = new Vector2[vertices.Length];
        int signX, signY, signZ;
        
        int i = 0;
        
        for (int iterationY = 0; iterationY < 2; iterationY++)
        {
            float Ucoord = 0;
            signY = (int)Mathf.Pow(-1,iterationY);
            for (int iterationZ = 0; iterationZ < 2; iterationZ++)
            {
                Ucoord += iterationZ * sizeZ;
                signZ = (int)Mathf.Pow(-1, iterationZ);
                for (int iterationX = 0; iterationX < 2; iterationX++)
                {
                    Ucoord += iterationX * sizeX;
                    signX = (int)Mathf.Pow(-1, iterationX);
                    var valueX = sizeX / 2 * signX;
                    var valueY = sizeY / 2 * (signY + 1);
                    var valueZ = sizeZ / 2 * signZ;
                    //*центр объекта относительно координат меша х и z в середине, относительно y в нижнем уровне.
                    // z  _____    Y  _____  
                    // | |     |   | |     |  
                    // | |  Х  |   | |     |  
                    // | |_____|   | |__Х__|  
                    // |_________x |_________x         Х- центр
                    vertices[i] = new Vector3(valueX, valueY, valueZ);                    
                    uvs[i] = new Vector2(Ucoord, valueY);
                    i++;
                }
            }
        }
        uvs[0].x = 0;
        uvs[1].x = sizeX;
        uvs[2].x = -sizeZ;
        uvs[3].x = sizeX-sizeZ;

        uvs[4].x = 0;
        uvs[5].x = sizeX;
        uvs[6].x = -sizeZ;
        uvs[7].x = sizeX - sizeZ;
    }
    private void GenerateTriangles()
    {
        triangles = new int[36];
        ////потолок
        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;

        //triangles[3] = 2;
        //triangles[4] = 1;
        //triangles[5] = 3;
        //стена 1 
        triangles[6] = 0;
        triangles[7] = 2;
        triangles[8] = 4;

        triangles[9] = 6;
        triangles[10] = 4;
        triangles[11] = 2;

        //стена 2
        triangles[12] = 4;
        triangles[13] = 5;
        triangles[14] = 0;

        triangles[15] = 5;
        triangles[16] = 1;
        triangles[17] = 0;

        //стена 3
        triangles[18] = 1;
        triangles[19] = 5;
        triangles[20] = 7;

        triangles[21] = 1;
        triangles[22] = 7;
        triangles[23] = 3;

        //стена 4
        triangles[24] = 3;
        triangles[25] = 7;
        triangles[26] = 6;

        triangles[27] = 3;
        triangles[28] = 6;
        triangles[29] = 2;

        ////пол
        //triangles[30] = 5;
        //triangles[31] = 6;
        //triangles[32] = 7;

        //triangles[33] = 5;
        //triangles[34] = 4;
        //triangles[35] = 6;
    }
    void SetData()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.name = "Room";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uvs;
        GetComponent<MeshCollider>().sharedMesh=mesh;
    }
    public void SetValues()
    {

    }
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.5f);
        }
    }
    // Update is called once per frame
    void Start()
    {
        //GenerateMesh();
    }
}
