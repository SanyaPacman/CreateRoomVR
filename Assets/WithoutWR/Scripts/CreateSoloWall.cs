using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshCollider))]
public class CreateSoloWall : MonoBehaviour
{
    public Transform startWall;
    public Transform endWall;
    public GameObject reverseWallgo;
    private float heigth=1;
    private Mesh meshWall;
    private Mesh meshReverseWall;
    private Vector3[] verticesWalls=new Vector3[4];
    private int[] trianglesWalls= new int[6];
    private Vector2[] uvsWalls=new Vector2[4];
    private void UpdateVerticesFromObjects()
    {
        //verticesWalls = new Vector3[objects.Length * 2 + 2];
       
        verticesWalls[0] = startWall.localPosition;
        verticesWalls[1] = startWall.localPosition + new Vector3(0, heigth, 0);
        verticesWalls[2] = endWall.localPosition;
        verticesWalls[3] = endWall.localPosition + new Vector3(0, heigth, 0);
    }
    private void CreateUvWalls()
    {
        uvsWalls = new Vector2[verticesWalls.Length];
        float uValue = 0;
        
        //u -ширина(x) v-высота (y)
        uvsWalls[0] = new Vector2(uValue, verticesWalls[0].y);
        for (int i = 1; i < verticesWalls.Length; i++)
        {
            Vector2 lastPointXYProjection = new Vector2(verticesWalls[i - 1].x, verticesWalls[i - 1].z);
            Vector2 currentPointXYProjection = new Vector2(verticesWalls[i].x, verticesWalls[i].z);
            uValue += Vector2.Distance(lastPointXYProjection, currentPointXYProjection);
            uvsWalls[i] = new Vector2(uValue, verticesWalls[i].y);
        }
    }
    private int[] ReverseAllTriangles(int[] triangles)
    {
        int[] res = triangles;
        res = new int[triangles.Length];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = triangles[i];
        }
        for (int i = 0; i < triangles.Length/3; i++)
        {
            res[i * 3] = triangles[i*3 + 1];
            res[i * 3+1] = triangles[i*3];
        }
        //for (int i = 0; i < triangles.Length; i++)
        //{
        //    Debug.Log(string.Format("<Color=red> i={0} value={1} </Color>", i, triangles[i]));
        //    Debug.Log(string.Format("<Color=green> i={0} value={1} </Color>", i, res[i]));
        //}
        return res;
    }
    private void SetTriangle(ref int[] triangles, int triangleIndex, int vertex1, int vertex2, int vertex3,  bool isReverse = false)
    {
        if (!isReverse)
        {
            triangles[triangleIndex*3] = vertex1;
            triangles[triangleIndex * 3+1] = vertex2;
            triangles[triangleIndex * 3+2] = vertex3;
        }
        else
        {
            triangles[triangleIndex * 3] = vertex2;
            triangles[triangleIndex * 3 + 1] = vertex1;
            triangles[triangleIndex * 3 + 2] = vertex3;
        }
    }
    /// <summary>
    /// создает стену по предыдущим четырем индексам точек относительно указанного
    /// </summary>
    /// <param name="indexVert"> индекс последней точки в стене </param>
    /// <param name="indexTriange"> индекс треугольника в меше</param>
    private int[] CreateWall(int indexVert,bool isReverse=false)
    {
        int[] res = new int[6];
        if (!isReverse)
        {
            SetTriangle(ref res,  0, 0, 1, 2);
            SetTriangle(ref res, 1, 2, 1, 3);
        }
        else
        {
            SetTriangle(ref res, 0, 0, 1, 2,true);
            SetTriangle(ref res, 1, 2, 1, 3,true);
        }
        return res;
    }
    private Mesh CreateMesh(Vector3[] vertices, Vector2[] uv, int[] triangles)
    {
        Mesh mesh = new Mesh();        
        mesh.name = "Room";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
        return mesh;
    }

    private void SetData(GameObject target, Mesh mesh)
    {
        
        target.GetComponent<MeshFilter>().mesh = mesh;
        target.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
    public void Start()
    {
        UpdateVerticesFromObjects();
        trianglesWalls = CreateWall(3);
        meshWall = CreateMesh(verticesWalls, uvsWalls, trianglesWalls);
        int[] reverseTriangles = ReverseAllTriangles(trianglesWalls);
        meshReverseWall = CreateMesh(verticesWalls, uvsWalls, reverseTriangles);
        SetData(this.gameObject,meshWall);
        SetData(reverseWallgo, meshReverseWall);
    }
    private void Update()
    {
        UpdateVerticesFromObjects();
        CreateUvWalls();
        meshWall = CreateMesh(verticesWalls, uvsWalls, trianglesWalls);
        meshReverseWall = CreateMesh(verticesWalls, uvsWalls, ReverseAllTriangles(trianglesWalls));
        SetData(this.gameObject, meshWall);
        SetData(reverseWallgo, meshReverseWall);
    }
}