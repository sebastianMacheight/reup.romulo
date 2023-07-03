using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin.Experiments
{
    public static class CreateMesh
    {
        public static Mesh GetTheMesh()
        {
            var vertices = new Vector3[]
            {
                new Vector3(0,0,0), new Vector3(1,0,0), new Vector3(0,1,0)
            };
            var trianges = new int[] {0,1,2};
            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = trianges;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            MeshUtils.GeneratePlanarUVMapping(mesh);
            return mesh;
        }
    }
}
