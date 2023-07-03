//using UnityEngine;

//public class PolygonReducer : MonoBehaviour
//{
//    public int targetPolygonCount = 1000; // Desired polygon count after reduction
//    public float reductionRatio = 0.5f; // Desired reduction ratio (0.0 to 1.0)

//    private MeshFilter meshFilter;

//    private void Start()
//    {
//        meshFilter = GetComponent<MeshFilter>();
//        ReducePolygons();
//    }

//    private void ReducePolygons()
//    {
//        if (meshFilter == null)
//        {
//            Debug.LogError("MeshFilter component not found!");
//            return;
//        }

//        Mesh mesh = meshFilter.mesh;
//        int currentPolygonCount = mesh.triangles.Length / 3; // Each triangle consists of 3 vertices

//        int targetVerticesCount = Mathf.RoundToInt(currentPolygonCount * reductionRatio);

//        if (targetVerticesCount <= 0 || targetVerticesCount >= mesh.vertices.Length)
//        {
//            Debug.LogWarning("Target vertices count is invalid. No reduction performed.");
//            return;
//        }

//        // Clustering process
//        while (mesh.vertices.Length > targetVerticesCount)
//        {
//            int bestVertexIndex = GetBestVertexIndex(mesh);

//            if (bestVertexIndex == -1)
//                break;

//            CollapseVertex(mesh, bestVertexIndex);
//        }

//        meshFilter.mesh = mesh;
//    }

//    private int GetBestVertexIndex(Mesh mesh)
//    {
//        Vector3[] vertices = mesh.vertices;
//        int[] triangles = mesh.triangles;

//        float bestDistance = float.MaxValue;
//        int bestVertexIndex = -1;

//        for (int i = 0; i < vertices.Length; i++)
//        {
//            if (!IsVertexMovable(mesh, i))
//                continue;

//            Vector3 vertex = vertices[i];
//            float distance = 0f;

//            for (int j = 0; j < triangles.Length; j += 3)
//            {
//                if (triangles[j] == i || triangles[j + 1] == i || triangles[j + 2] == i)
//                {
//                    Vector3 centroid = (vertices[triangles[j]] + vertices[triangles[j + 1]] + vertices[triangles[j + 2]]) / 3f;
//                    distance += Vector3.Distance(vertex, centroid);
//                }
//            }

//            if (distance < bestDistance)
//            {
//                bestDistance = distance;
//                bestVertexIndex = i;
//            }
//        }

//        return bestVertexIndex;
//    }

//    private bool IsVertexMovable(Mesh mesh, int vertexIndex)
//    {
//        int[] triangles = mesh.triangles;
//        int triangleCount = triangles.Length / 3;

//        for (int i = 0; i < triangleCount; i++)
//        {
//            int triangleIndex = i * 3;

//            if (triangles[triangleIndex] == vertexIndex || triangles[triangleIndex + 1] == vertexIndex || triangles[triangleIndex + 2] == vertexIndex)
//            {
//                // Check if the vertex is only part of a single triangle
//                if (!IsTriangleUnique(triangles, triangleIndex, vertexIndex))
//                    return false;
//            }
//        }

//        return true;
//    }

//    private bool IsTriangleUnique(int[] triangles, int triangleIndex, int vertexIndex)
//    {
//        int triangleVertexCount = 0;

//        for (int i = 0; i < triangles.Length; i++)
//        {
//            if (triangles[i] == vertexIndex)
//            {
//                triangleVertexCount++;

//                // If there is more than one occurrence of the vertex in the triangle, it is not unique
//                if (triangleVertexCount > 1)
//                    return false;
//            }
//        }

//        return true;
//    }

//    private void CollapseVertex(Mesh mesh, int vertexIndex)
//    {
//        Vector3[] vertices = mesh.vertices;
//        int[] triangles = mesh.triangles;

//        Vector3 vertex = vertices[vertexIndex];

//        for (int i = 0; i < triangles.Length; i++)
//        {
//            if (triangles[i] == vertexIndex)
//                triangles[i] = 0; // Replace with another valid index (0 in this example)
//            else if (triangles[i] > vertexIndex)
//                triangles[i]--; // Adjust indices due to vertex removal
//        }

//        mesh.vertices = RemoveAt(vertices, vertexIndex);
//        mesh.triangles = RemoveAt(triangles, -1); // Remove -1 entries (representing the collapsed vertex)

//        mesh.RecalculateBounds();
//        mesh.RecalculateNormals();
//    }

//    private T[] RemoveAt<T>(T[] array, int index)
//    {
//        T[] newArray = new T[array.Length - 1];

//        for (int i = 0, j = 0; i < array.Length; i++)
//        {
//            if (i != index)
//            {
//                newArray[j] = array[i];
//                j++;
//            }
//        }

//        return newArray;
//    }
//}

using UnityEngine;

public class PolygonReducer : MonoBehaviour
{
    public int targetPolygonCount = 1000; // Desired polygon count after reduction
    public float reductionRatio = 0.5f; // Desired reduction ratio (0.0 to 1.0)

    private MeshFilter meshFilter;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        ReducePolygons();
    }

    private void ReducePolygons()
    {
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component not found!");
            return;
        }

        Mesh mesh = meshFilter.mesh;
        int currentPolygonCount = mesh.triangles.Length / 3; // Each triangle consists of 3 vertices

        int targetVerticesCount = Mathf.RoundToInt(currentPolygonCount * reductionRatio);

        if (targetVerticesCount <= 0 || targetVerticesCount >= mesh.vertices.Length)
        {
            Debug.LogWarning("Target vertices count is invalid. No reduction performed.");
            return;
        }

        // Clustering process
        while (mesh.vertices.Length > targetVerticesCount)
        {
            int bestVertexIndex = GetBestVertexIndex(mesh);

            if (bestVertexIndex == -1)
                break;

            CollapseVertex(mesh, bestVertexIndex);
        }

        meshFilter.mesh = mesh;
    }

    private int GetBestVertexIndex(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        int vertexCount = vertices.Length;
        int triangleCount = triangles.Length / 3;

        float bestDistance = float.MaxValue;
        int bestVertexIndex = -1;

        // Calculate vertex to centroid distances
        float[] distances = new float[vertexCount];
        int[] triangleVertexCounts = new int[vertexCount];

        for (int i = 0; i < triangleCount; i++)
        {
            int triangleIndex = i * 3;
            int vertexIndexA = triangles[triangleIndex];
            int vertexIndexB = triangles[triangleIndex + 1];
            int vertexIndexC = triangles[triangleIndex + 2];

            Vector3 centroid = (vertices[vertexIndexA] + vertices[vertexIndexB] + vertices[vertexIndexC]) / 3f;

            distances[vertexIndexA] += Vector3.Distance(vertices[vertexIndexA], centroid);
            distances[vertexIndexB] += Vector3.Distance(vertices[vertexIndexB], centroid);
            distances[vertexIndexC] += Vector3.Distance(vertices[vertexIndexC], centroid);

            triangleVertexCounts[vertexIndexA]++;
            triangleVertexCounts[vertexIndexB]++;
            triangleVertexCounts[vertexIndexC]++;
        }

        // Find best vertex to collapse
        for (int i = 0; i < vertexCount; i++)
        {
            if (!IsVertexMovable(triangleVertexCounts[i]))
                continue;

            float distance = distances[i] / triangleVertexCounts[i];

            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestVertexIndex = i;
            }
        }

        return bestVertexIndex;
    }

    private bool IsVertexMovable(int triangleVertexCount)
    {
        return triangleVertexCount <= 1;
    }

    private void CollapseVertex(Mesh mesh, int vertexIndex)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        Vector3 vertex = vertices[vertexIndex];

        // Find triangles containing the vertex
        int[] trianglesContainingVertex = FindTrianglesContainingVertex(triangles, vertexIndex);

        // Remove the vertex from the triangles
        foreach (int triangleIndex in trianglesContainingVertex)
        {
            RemoveVertexFromTriangle(triangles, triangleIndex, vertexIndex);
        }

        // Remove collapsed vertex and adjust triangle indices
        int vertexCount = vertices.Length;

        for (int i = vertexIndex + 1; i < vertexCount; i++)
        {
            vertices[i - 1] = vertices[i];
        }

        vertices[vertexCount - 1] = vertex;

        for (int i = 0; i < triangles.Length; i++)
        {
            int triangleVertexIndex = triangles[i];

            if (triangleVertexIndex > vertexIndex)
            {
                triangles[i] = triangleVertexIndex - 1;
            }
        }

        // Resize the arrays
        mesh.vertices = RemoveLastElement(vertices);
        mesh.triangles = RemoveTriangles(triangles, trianglesContainingVertex);

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    private int[] FindTrianglesContainingVertex(int[] triangles, int vertexIndex)
    {
        int triangleCount = triangles.Length / 3;
        int[] trianglesContainingVertex = new int[triangleCount];
        int count = 0;

        for (int i = 0; i < triangleCount; i++)
        {
            int triangleIndex = i * 3;

            if (triangles[triangleIndex] == vertexIndex || triangles[triangleIndex + 1] == vertexIndex || triangles[triangleIndex + 2] == vertexIndex)
            {
                trianglesContainingVertex[count] = i;
                count++;
            }
        }

        System.Array.Resize(ref trianglesContainingVertex, count);

        return trianglesContainingVertex;
    }

    private void RemoveVertexFromTriangle(int[] triangles, int triangleIndex, int vertexIndex)
    {
        int triangleVertexIndex = triangleIndex * 3;

        if (triangles[triangleVertexIndex] == vertexIndex)
        {
            triangles[triangleVertexIndex] = triangles[triangleVertexIndex + 1];
        }
        else if (triangles[triangleVertexIndex + 1] == vertexIndex)
        {
            triangles[triangleVertexIndex + 1] = triangles[triangleVertexIndex + 2];
        }

        triangles[triangleVertexIndex + 2] = -1;
    }

    private Vector3[] RemoveLastElement(Vector3[] array)
    {
        int length = array.Length;
        Vector3[] newArray = new Vector3[length - 1];

        for (int i = 0; i < length - 1; i++)
        {
            newArray[i] = array[i];
        }

        return newArray;
    }

    private int[] RemoveTriangles(int[] triangles, int[] trianglesToRemove)
    {
        int triangleCount = triangles.Length / 3;
        int[] newTriangles = new int[triangles.Length - trianglesToRemove.Length * 3];
        int count = 0;

        for (int i = 0; i < triangleCount; i++)
        {
            if (!Contains(trianglesToRemove, i))
            {
                int triangleIndex = i * 3;

                newTriangles[count] = triangles[triangleIndex];
                newTriangles[count + 1] = triangles[triangleIndex + 1];
                newTriangles[count + 2] = triangles[triangleIndex + 2];

                count += 3;
            }
        }

        return newTriangles;
    }

    private bool Contains(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }

        return false;
    }
}

