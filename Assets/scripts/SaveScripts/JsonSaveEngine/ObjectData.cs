using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ObjectData
{
	public Vector3[] meshVertices = null;
	public List<int> submeshesTriangles;
	public List<int> submeshesLenghts;
	public Vector3 position;
	public Vector3 localScale;
	public string name = null;
	public MaterialData[] materialsData;

	public ObjectData(GameObject o)
	{
		name = o.name;
		position = o.transform.position;
		localScale= o.transform.localScale;
		if (o.TryGetComponent<MeshFilter>(out var meshFilter))
		{
			var mesh = meshFilter.mesh;
			meshVertices = mesh.vertices;
			submeshesLenghts = new List<int>();
			submeshesTriangles = new List<int>();
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				var submesh = mesh.GetTriangles(i);
				foreach(var v in submesh)
				{
					submeshesTriangles.Add(v);
			    }
				submeshesLenghts.Add(submesh.Length);
			}
	    }
		if(o.TryGetComponent<Renderer>(out var renderer))
		{
			if (renderer.materials.Length > 0)
			{
				ExtractMaterialsData(renderer.materials);
			}
	    }
	}

	public GameObject LoadObjectData()
	{ 
        GameObject obj = new GameObject();
		LoadObjectData(obj);
        return obj;
	}

	public void LoadObjectData(GameObject obj)
	{
		obj.name = name;
		obj.transform.position = position;
		if (submeshesLenghts.Count > 0 && meshVertices.Length > 0)
		{ 
			var meshRenderer = obj.AddComponent<MeshRenderer>();
			var meshFilter = obj.AddComponent<MeshFilter>();
			meshFilter.mesh.SetVertices(meshVertices);
			var materials = new Material[submeshesLenghts.Count];
			int startIndex = 0;
			int endIndex = submeshesLenghts[0];
			meshFilter.mesh.subMeshCount = submeshesLenghts.Count;
			for(int i = 0; i<submeshesLenghts.Count; i++)
			{
				meshFilter.mesh.SetTriangles(submeshesTriangles.GetRange(startIndex, endIndex), i);
				materials[i] = materialsData[i].LoadMaterialData();
				startIndex += endIndex;
				endIndex = (i < submeshesLenghts.Count - 1) ? submeshesLenghts[i + 1] : 0;

			}
			meshRenderer.materials = materials;
			meshFilter.mesh.RecalculateNormals();
			meshFilter.mesh.RecalculateBounds();
	    }
		obj.transform.localScale = localScale;
	}

	private void ExtractMaterialsData(Material[] materials)
	{
		var materialsCount = materials.Length;
		materialsData = new MaterialData[materialsCount];
		for (int i=0; i<materialsCount; i++)
		{
            materialsData[i] = new MaterialData(materials[i]);
        }
    }
}

