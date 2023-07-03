using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using ReUpVirtualTwin.Helpers;

public class MeshInfo
{
    public bool isMesh = false;
    public bool isMeshSerialized = false;
    public Vector3? meanVertex = null;
    public Vector3[] borders = null;
    public Vector3? position = null;
    public Vector3 size = Vector3.zero;
    public float volumen = 0;
    public float area = 0;
    public float volVertexDensity;
    public float arealVertexDensity;
    public long vertexCount;
    public long triangleCount;


    public MeshInfo(GameObject obj, bool readSerializedMesh = false)
    {
        //Debug.Log($"MeshInfo for {obj.name}");
        SerializeMesh serializeMesh = null;
        Mesh mesh;

        if (obj.GetComponent<MeshFilter>() == null)
        {
            isMesh = false;
            return;
        }
        isMesh = true;

        if (readSerializedMesh)
        {
            serializeMesh = obj.GetComponent<SerializeMesh>();
        }
        if (serializeMesh != null)
        {
            mesh = serializeMesh.Rebuild();
            isMeshSerialized = true;
        }
        else
        {
            mesh = obj.GetComponent<MeshFilter>().sharedMesh;
        }
        //Debug.Log($"mesh is {mesh}");
        //Debug.Log($"mesh Vertex count is is {mesh.vertexCount}");
        //Debug.Log($"mesh Vertex count is is {mesh.vertices.Length}");
        //position = obj.transform.position;
        //meanVertex = MeshUtils.MeanPoint(obj);
        borders = MeshUtils.Borders(mesh, obj.transform);
        //Debug.Log($"borders are null {borders == null}");
        if (borders == null) return;
        size = MeshUtils.Size(borders);
        //Debug.Log($"borders are  {borders[0]} {borders[1]}");
        //Debug.Log($"size : {size}");
        //Debug.Log(string.Join(", ", borders));
        vertexCount = mesh.vertexCount;
        triangleCount = mesh.triangles.Length / 3;
        volVertexDensity = MeshUtils.VolumetricVertexDensity(vertexCount, size);
        arealVertexDensity = MeshUtils.ArealVertexDensity(vertexCount, size);
        area = MeshUtils.Area(size);
        volumen = MeshUtils.Volumen(size);
    }
    //public void OnSceneGUI(SceneView sceneView)
    //{
    //    var minSize = Mathf.Min(size.x, size.y, size.z);
    //    Handles.color = Color.red;
    //    DrawSphereGizmo((Vector3)meanVertex + (Vector3)position, minSize/ 5);
    //    //DrawBorders();
    //}
    //private void DrawSphereGizmo(Vector3 mean, float radius)
    //{
    //    Handles.DrawWireDisc(mean, Vector3.up, radius);
    //    Handles.DrawWireDisc(mean, Vector3.right, radius);
    //    Handles.DrawWireDisc(mean, Vector3.forward, radius);
    //}

    //private void DrawBorders()
    //{

    //    var center = new Vector3(
    //        (borders[0].x + borders[1].x) / 2,
    //        (borders[0].y + borders[1].y) / 2,
    //        (borders[0].z + borders[1].z) / 2
    //        );
    //    Handles.DrawWireCube(center + (Vector3)position, size);
    //}
}
