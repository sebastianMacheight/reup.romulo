using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MaterialData
{
    public float colorR;
    public float colorG;
    public float colorB;
    public float colorA;

    public MaterialData(Material material)
    {
        colorR = material.color.r;
        colorG = material.color.g;
        colorB = material.color.b;
        colorA = material.color.a;
	}
    public Material LoadMaterialData()
    {
        var material = new Material(Shader.Find("Standard"));
        LoadMaterialData(material);
        return material;
	}
    public void LoadMaterialData(Material material)
    {
        material.color = new Color(colorR, colorG, colorB, colorA);
	}
}
