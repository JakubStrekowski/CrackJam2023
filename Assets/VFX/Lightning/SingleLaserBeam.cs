using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SingleLaserBeam : MonoBehaviour
{
    public int PointCount = 2;
    public float Distance = 5f;
    public float Width = .2f;
    [ColorUsage(false, true)] public Color Color;
    
    private static readonly int ColorId = Shader.PropertyToID("_Color");

    private void OnEnable()
    {
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnDisable()
    {
        GetComponent<LineRenderer>().enabled = false;
    }

    private void Update()
    {
        var renderer = GetComponent<LineRenderer>();
        renderer.positionCount = PointCount;
        var div = (float) (PointCount - 1);
        for (int i = 0; i < PointCount; i++)
        {
            var value = i / div;
            renderer.SetPosition(i, transform.position + transform.forward * Distance * value);
        }

        renderer.widthMultiplier = Width;

        var propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetColor(ColorId, Color);
        
        renderer.SetPropertyBlock(propertyBlock);
    }
}
