using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshCollider))]
public class ProceduralWallGeneration : MonoBehaviour
{
    /// <summary>
    /// Объекты на основе которых будет строиться меш. Должны быть дочерними относительно хозяина скрипта
    /// Для создания комнаты объекты должны располагаться по часовой клетке
    /// </summary>
    //[Header ("Скрипт генерирует стену с указанной высотой. За основу стены берутся дочерние объекты. Чтобы стены смотрели внутрь, дочерние объекты должны располагаться по часовой стрелке")]

    private Transform[] objects;
    [SerializeField]
    private GameObject floor;
    [SerializeField] 
    private GameObject ceiling;//потолок
    [SerializeField]
    private float heigth;
    private Vector3[] verticesWalls;
    private int[] trianglesWalls;
    private Vector2[] uvsWalls;

    private Vector3[] verticesFloor;
    private int[] trianglesFloor;
    private Vector2[] uvsFloor;

    private Vector3[] verticesCeiling;
    private int[] trianglesCeiling;
    private Vector2[] uvsCeiling;

    public void SetHeigth(float _height)
    {
        heigth = Mathf.Abs( _height);
    }

    public void TrySetHeigth(string _height)
    {
        float res;
        if (float.TryParse(_height, out res))
        {
            heigth = Mathf.Abs(res);
        }
        
        
    }
    
    /// <summary>
    /// Создает сектор стены по указанной точке и предыдущей
    /// </summary>
    private void CreateWall(int indexVert, int indexTriange)
    {
        indexVert++;
        trianglesWalls[indexTriange] = indexVert - 3;
        trianglesWalls[indexTriange+1] = indexVert - 2;
        trianglesWalls[indexTriange+2] = indexVert - 1;

        trianglesWalls[indexTriange+3] = indexVert - 1;
        trianglesWalls[indexTriange+4] = indexVert - 2;
        trianglesWalls[indexTriange+5] = indexVert;
    }
    private void UpdateVerticesFromObjects()
    {
        verticesWalls = new Vector3[objects.Length * 2 + 2];
        for (int i = 0; i < objects.Length; i++)
        {
            verticesWalls[2 * i] = objects[i].localPosition;
            verticesWalls[2 * i + 1] = objects[i].localPosition + new Vector3(0, heigth, 0);            
        }        
        int endVertices = verticesWalls.Length - 1;
        verticesWalls[endVertices-1] = verticesWalls[0];
        verticesWalls[endVertices] = verticesWalls[1];
    }
    private void GenerateWalls()
    {
        trianglesWalls = new int[verticesWalls.Length * 3];       
        //создаем все стены кроме последней
        for (int i = 2; i < verticesWalls.Length; i += 2)
        {
            CreateWall(i, (i - 1) * 3);
        }                         
    }
   
    /// <summary>
    /// Получить четные/нечетные элементы массива
    /// </summary>
    /// <param name="vertices"></param>
    /// <param name="isEvenNumbers"></param>
    /// <returns></returns>
    private Vector3[] GetEvensVertices(Vector3[] vertices, bool isEvenNumbers)
    {
        int add=0;
        if (!isEvenNumbers)
            add = 1;
        var result = new Vector3[vertices.Length / 2];
        for (int i = 0; i < result.Length-1; i++)
        {
            result[i] = vertices[2 * i + add];
        }
        return result;
    }
    
    private Vector2[] UpdateUv(Vector3[] vertices)
    {
        var result= new Vector2[vertices.Length];
        for (int i = 0; i < result.Length-1; i++)
        {
            result[i] = new Vector2(vertices[i].x,vertices[i].z);
        }
        return result;
    }
    private int[] CreatePlane(int vertexNumber, bool around)
    {     
        var result =new int[(vertexNumber-2)*3];
        for (int i = 0; i < vertexNumber-2; i++)
        {
            result[i * 3] = 0;
            if (around)
            {
                result[i * 3 + 1] = i +1;
                result[i * 3 + 2] = i;
            }
            else
            {
                result[i * 3 + 1] = i;
                result[i * 3 + 2] = i + 1;
            }            
        }
        return result;
    }
    private void CreateWalls()
    {
        GetObjectsFromChilds();
        UpdateVerticesFromObjects();
        GenerateWalls();
        CreateUvWalls();
        SetData(this.gameObject, verticesWalls, uvsWalls, trianglesWalls);

    }
    private void UpdateFloor()
    {
        verticesFloor = GetEvensVertices(verticesWalls, true);
        uvsFloor = UpdateUv(verticesFloor);
        SetData(floor, verticesFloor, uvsFloor, trianglesFloor);
    }
    private void  CreateFloor()
    {
        verticesFloor = GetEvensVertices(verticesWalls, true);
        uvsFloor = UpdateUv(verticesFloor);
        trianglesFloor = CreatePlane(verticesFloor.Length, false);
        SetData(floor, verticesFloor, uvsFloor, trianglesFloor);
    }

    private void UpdateCeiling()
    {
        verticesCeiling = GetEvensVertices(verticesWalls, false);
        uvsCeiling = UpdateUv(verticesCeiling);
        SetData(ceiling, verticesCeiling, uvsCeiling, trianglesCeiling);
    }
    private void CreateCeiling()
    {
        verticesCeiling = GetEvensVertices(verticesWalls, false);
        uvsCeiling = UpdateUv(verticesCeiling);
        trianglesCeiling = CreatePlane(verticesCeiling.Length, true);
        SetData(ceiling, verticesCeiling, uvsCeiling, trianglesCeiling);
    }
    // private 
    private void CreateUvWalls()
    {
        uvsWalls = new Vector2[verticesWalls.Length];
        float uValue = 0;
        float vValue = 0;
        //u -ширина(x) v-высота (y)
        uvsWalls[0] = new Vector2(uValue, verticesWalls[0].y);        
        for (int i = 1; i < verticesWalls.Length; i++)
        {
            Vector2 lastPointXYProjection = new Vector2(verticesWalls[i - 1].x, verticesWalls[i - 1].z);
            Vector2 currentPointXYProjection = new Vector2(verticesWalls[i].x, verticesWalls[i].z);
            uValue += Vector2.Distance(lastPointXYProjection, currentPointXYProjection);
            uvsWalls[i] = new Vector2(uValue,verticesWalls[i].y);                        
        }
    }
    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        if (verticesWalls == null||verticesWalls.Length==0)
            return;
        for (int i = 0; i < verticesWalls.Length; i++)
        {
            Gizmos.DrawSphere(verticesWalls[i], 0.5f);
        }        
    }
    
    private void SetData(GameObject target, Vector3[] vertices, Vector2[] uv,int[] triangles )
    {
        Mesh mesh = new Mesh();
        target.GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "Room";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
        target.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
    
    void Start()
    {
        CreateWalls();
        CreateFloor();
        CreateCeiling();
    }
    
    private void GetObjectsFromChilds()
    {
        objects = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            objects[i] = transform.GetChild(i).transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Start();
    }
}
