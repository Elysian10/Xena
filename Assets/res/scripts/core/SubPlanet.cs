using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlanet : MonoBehaviour
{
    public List<Vector3> vertecyList;
    Vector3[] vertecies;
    List<int> triangleList;
    int[] triangles;
    Mesh mesh;
        
    ComputeBuffer buffer;

    struct DataStruct{
        public float x;
        public float y;
        public float z;
    }

    struct SubdivideTriangleStruct{
        public Vector3 first;
        public Vector3 section;
    }
    public void Init(int v1Index, int v2Index, int v3Index, int divisor){
        transform.gameObject.AddComponent(typeof(MeshFilter));
        transform.gameObject.AddComponent(typeof(MeshRenderer));
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer parentMeshRenderer = (MeshRenderer) transform.root.GetComponent(typeof(MeshRenderer));
        MeshRenderer meshRenderer = (MeshRenderer) GetComponent(typeof(MeshRenderer));
        meshRenderer.sharedMaterial = parentMeshRenderer.sharedMaterial;
        vertecyList = new List<Vector3>();
        triangleList = new List<int>();

        Planet planet = transform.root.gameObject.GetComponent<Planet>();
        Vector3 v1 = planet.vertecies[v1Index];
        Vector3 v2 = planet.vertecies[v2Index];
        Vector3 v3 = planet.vertecies[v3Index];
        {
            int tmp = divisor + 1;
            vertecies = new Vector3[tmp * (tmp + 1) / 2];
        }
        triangles = new int[divisor * divisor * 3];
        vertecies[0] = v1;
        Vector3 section = (v2 - v3) / (divisor);
        Vector3 leftEdge = (v3 - v1) / (divisor);
        Vector3 rightEdge = (v2 - v1) / (divisor);


        for (int i = 1; i <= divisor; i++){
            Vector3 first = leftEdge * i + v1;
            vertecies[i * (i + 1) / 2] = first;

            int j = 1;
            while (true){
                int vertexIndex = i * (i + 1) / 2 + j;
                int triIndex = 3*(i*(i-2)+2*j-1);
                vertecies[vertexIndex] = section * j + first;
                triangles[triIndex] = vertexIndex - i - 1;
                triangles[triIndex + 1] = vertexIndex;
                triangles[triIndex + 2] = vertexIndex - 1;

                j++;
                if (j > i)
                    break;
                    
                triangles[triIndex + 3] = vertexIndex - i - 1;
                triangles[triIndex + 4] = vertexIndex - i;
                triangles[triIndex + 5] = vertexIndex;
            }
        }


        /*for (int i = 1; i <= divisor; i++){
            Vector3 first = leftEdge * i + v1;
            vertecies[verteciesIndex++] = first;

            int j = 1;
            while (true){
                vertecies[verteciesIndex++] = section * j + first;
                triangles[triangleIndex++] = verteciesIndex - i - 2;
                triangles[triangleIndex++] = verteciesIndex - 1;
                triangles[triangleIndex++] = verteciesIndex - 2;

                j++;
                if (j > i)
                    break;
                    
                triangles[triangleIndex++] = verteciesIndex - i - 2;
                triangles[triangleIndex++] = verteciesIndex - i - 1;
                triangles[triangleIndex++] = verteciesIndex - 1;
            }
        }*/
        setHeight();
        apply();
    }

    void setHeight(){
        Planet planet = transform.root.gameObject.GetComponent<Planet>();
        ComputeShader shader = planet.shader;
        int handle = shader.FindKernel("SetRadius");
        ComputeBuffer buffer = new ComputeBuffer(vertecies.Length, sizeof(float) * 3);     
        buffer.SetData(vertecies); 
        shader.SetBuffer(handle,"vertecies", buffer);
        shader.SetFloat("radius", planet.radius);
        shader.Dispatch(handle,vertecies.Length / 4,1,1);
        buffer.GetData(vertecies);
        buffer.Dispose();
    }

    void apply(){
        mesh.Clear();
        mesh.vertices = vertecies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        transform.gameObject.AddComponent(typeof(Rigidbody));
        Rigidbody rigidbody = (Rigidbody) transform.gameObject.GetComponent(typeof(Rigidbody));
        rigidbody.isKinematic = true;
        transform.gameObject.AddComponent(typeof(MeshCollider));
        MeshCollider meshCollider = (MeshCollider)  transform.gameObject.GetComponent(typeof(MeshCollider));
        meshCollider.sharedMesh = mesh;

    }
}
