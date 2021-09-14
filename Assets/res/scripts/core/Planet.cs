using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Planet : MonoBehaviour
{
    [Range(10.0f, 10000.0f)]
    public float radius;
    public int factor;
    Mesh mesh;
    public List<Vector3> vertecies;
    List<int> triangles;
    float goldenDiameter = 1 + (((1 + Mathf.Sqrt(5)) * (1 + Mathf.Sqrt(5))) / 4);
    public ComputeShader shader;
    public ComputeShader subdivideTriangleShader;

    void init(float radius, int factor){
        transform.gameObject.AddComponent(typeof(Planet));

        MeshRenderer parentMeshRenderer = (MeshRenderer) transform.parent.GetComponent(typeof(MeshRenderer));
        MeshRenderer meshRenderer = (MeshRenderer) GetComponent(typeof(MeshRenderer));
        meshRenderer.materials = parentMeshRenderer.materials;

        this.radius = radius;
        this.factor = factor;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        triangles = new List<int>();
    }
    void Start()
    {
    }

    public void TestStart()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateIcosahedron();
    }


    void CreateIcosahedron()
    {
        for (int i = transform.childCount; i > 0; --i)
            DestroyImmediate(transform.GetChild(0).gameObject);
        float diameter = radius * 2;
        float dist = radius * Constants.goldenRatio;

        vertecies = new List<Vector3>{
            new Vector3(diameter,dist,0),//0
            new Vector3(-diameter,dist,0),//1
            new Vector3(0,diameter,dist),//2
            new Vector3(0,diameter,-dist),//3
            new Vector3(dist,0,-diameter),//4
            new Vector3(-dist,0,-diameter),//5
            new Vector3(-dist,0,diameter),//6
            new Vector3(dist,0,diameter),//7
            new Vector3(0,-diameter,dist),//8
            new Vector3(0,-diameter,-dist),//9
            new Vector3(diameter,-dist,0),//10
            new Vector3(-diameter,-dist,0)//11
        };

        triangles = new List<int>
        {
            0,1,2,
            0,3,1,
            0,7,4,
            0,4,3,
            1,3,5,
            1,6,2,
            0,2,7,
            1,5,6,
            2,6,8,
            2,8,7,
            4,7,10,
            3,4,9,
            3,9,5,
            5,11,6,
            6,11,8,
            7,8,10,
            4,10,9,
            5,9,11,
            9,10,11,
            8,11,10

        };
        int index = 0;
        while (true){
            createSubdividedChild(triangles[index],triangles[index + 1], triangles[index + 2], factor);
            index += 3;
            if (index > triangles.Count - 1)
             break;
        }
        //updateMesh();
        
        
    }

    void updateMesh(){
        mesh.Clear();
        mesh.vertices = vertecies.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        Debug.Log("vertecies: " + vertecies.Count);
        Debug.Log("triangle points : " + triangles.Count);
    }

    void createSubdividedChild(int v1Index, int v2Index, int v3Index, int divisor){
        GameObject child = new GameObject("child");
        child.transform.parent = transform;
        child.AddComponent(typeof(SubPlanet));
        SubPlanet ss = (SubPlanet) child.GetComponent(typeof(SubPlanet));
        ss.Init(v1Index, v2Index, v3Index, divisor);
    }
    void setHeight(){
        for (int i = 0; i < vertecies.Count; i++){
            vertecies[i] = vertecies[i].normalized * radius;
        }
    }
}
