using System.Linq;
using UnityEngine;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.helpers
{
    public static class ReupMeshUtils
    {
        #nullable enable
        public static ObjectBorder? GetObjectTreeBorder(GameObject parent)
        {
            ObjectBorder? parentBorder = null;
            MeshFilter parentMeshFilter = parent.GetComponent<MeshFilter>();
            if (parentMeshFilter == null && parent.transform.childCount == 0)
            {
                return null;
            }
            if (parentMeshFilter != null)
            {
                parentBorder = GetObjectBorder(parent);
            }
            foreach (Transform child in parent.transform)
            {
                ObjectBorder? childBorder = GetObjectTreeBorder(child.gameObject);
                parentBorder = ExtendBorder(parentBorder, childBorder);
            }
            return parentBorder;
        }
        public static ObjectBorder? ExtendBorder(ObjectBorder parentBorder, ObjectBorder childBorder)
        {
            if (parentBorder == null && childBorder == null)
            {
                return null;
            }
            if (parentBorder == null)
            {
                return childBorder;
            }
            if (childBorder == null)
            {
                return parentBorder;
            }
            return new ObjectBorder
            {
                maxBorders = new Vector3
                    (
                        FindMax(parentBorder.maxBorders.x, childBorder.maxBorders.x),
                        FindMax(parentBorder.maxBorders.y, childBorder.maxBorders.y),
                        FindMax(parentBorder.maxBorders.z, childBorder.maxBorders.z)
                    ),
                minBorders = new Vector3
                    (
                        FindMin(parentBorder.minBorders.x, childBorder.minBorders.x),
                        FindMin(parentBorder.minBorders.y, childBorder.minBorders.y),
                        FindMin(parentBorder.minBorders.z, childBorder.minBorders.z)
                    )
            };
        }
        private static float FindMin(float a, float b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }
        private static float FindMax(float a, float b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }
        public static Vector3 MeanPointOfObject(GameObject obj)
        {
            var transform = obj.transform;
            var vertices = obj.GetComponent<MeshFilter>().sharedMesh.vertices;
            Vector3 mean = vertices.Aggregate((curr, next) => curr + next) / vertices.Length;
            mean.x = mean.x * transform.GetTotalScale().x;
            mean.y = mean.y * transform.GetTotalScale().y;
            mean.z = mean.z * transform.GetTotalScale().z;
            mean = Quaternion.Euler(transform.localEulerAngles) * mean;
            return mean;
        }

        public static float Volumen(Vector3 size)
        {
            return (size.x * size.y * size.z);
        }

        public static float VolumetricVertexDensity(long vertexNumber, Vector3 size)
        {
            var density = vertexNumber / Volumen(size);
            return density;
        }

        public static float Area(Vector3 size)
        {
            return (size.x * size.y * 2 + size.x * size.z * 2 + size.y * size.z * 2);
        }

        public static float ArealVertexDensity(long vertexNumber, Vector3 size)
        {
            var density = vertexNumber / Area(size);
            return density;
        }

        public static Vector3 Size(Vector3[] borders)
        {
            var size = new Vector3(
                borders[1].x - borders[0].x,
                borders[1].y - borders[0].y,
                borders[1].z - borders[0].z
                );
            return size;
        }

        public static ObjectBorder? GetObjectBorder(GameObject obj)
        {
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter == null) return null;
            Transform transform = obj.transform;
            return GetObjectBorder(meshFilter.sharedMesh, transform);
        }
        public static ObjectBorder? GetObjectBorder(Mesh mesh, Transform transform)
        {
            Vector3[] vertices = mesh.vertices;

            if (vertices == null || vertices.Length == 0)
            {
                return null;
            }

            Vector3[] transformedVertices = new Vector3[vertices.Length];

            Quaternion rotation = Quaternion.Euler(transform.localEulerAngles);
            for(int i=0;  i<vertices.Length; i++)
            {
                Vector3 originalVertex = vertices[i];
                Vector3 scaledVectex = MultiplyVectors(originalVertex, transform.localScale);
                Vector3 rotatedScaledVertex = rotation * scaledVectex;
                transformedVertices[i] = rotatedScaledVertex;
            }

            float xmax = transformedVertices[0].x;
            float ymax = transformedVertices[0].y;
            float zmax = transformedVertices[0].z;
            float xmin = transformedVertices[0].x;
            float ymin = transformedVertices[0].y;
            float zmin = transformedVertices[0].z;
            foreach (Vector3 rotatedVertex in transformedVertices)
            {
                if (rotatedVertex.x < xmin) xmin = rotatedVertex.x;
                if (rotatedVertex.x > xmax) xmax = rotatedVertex.x;
                if (rotatedVertex.y < ymin) ymin = rotatedVertex.y;
                if (rotatedVertex.y > ymax) ymax = rotatedVertex.y;
                if (rotatedVertex.z < zmin) zmin = rotatedVertex.z;
                if (rotatedVertex.z > zmax) zmax = rotatedVertex.z;
            }

            var minBorder = new Vector3(xmin, ymin, zmin);
            var maxBorder = new Vector3(xmax, ymax, zmax);

            Debug.Log($"borders of {transform.name} before rotation:");
            Debug.Log($"min Borders: {minBorder}");
            Debug.Log($"max Borders: {maxBorder}");

            return new ObjectBorder
            {
                minBorders = minBorder,
                maxBorders = maxBorder
            };
        }

        public static void GeneratePlanarUVMapping(Mesh mesh)
        {
            var vertexCount = mesh.vertices.Length;
            var vertices = mesh.vertices;
            Vector2[] uvCoords = new Vector2[vertexCount];

            // Generate UV coordinates
            // Here's a simple example of planar mapping
            Vector3 minBounds = new Vector3(float.MaxValue, 0f, float.MaxValue);
            Vector3 maxBounds = new Vector3(float.MinValue, 0f, float.MinValue);

            for (int i = 0; i < vertexCount; i++)
            {
                Vector3 vertex = vertices[i];
                minBounds = Vector3.Min(minBounds, vertex);
                maxBounds = Vector3.Max(maxBounds, vertex);
            }

            for (int i = 0; i < vertexCount; i++)
            {
                Vector3 vertex = vertices[i];
                Vector2 uv = new Vector2(
                    Mathf.InverseLerp(minBounds.x, maxBounds.x, vertex.x),
                    Mathf.InverseLerp(minBounds.z, maxBounds.z, vertex.z)
                );
                uvCoords[i] = uv;
            }

            // Assign UV coordinates to the mesh
            mesh.uv = uvCoords;
        }
        static Vector3 MultiplyVectors(Vector3 v1, Vector3 v2)
        {
            float x = v1.x * v2.x;
            float y = v1.y * v2.y;
            float z = v1.z * v2.z;

            return new Vector3(x, y, z);
        }
    }
}
