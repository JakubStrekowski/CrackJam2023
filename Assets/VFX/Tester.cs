using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [ContextMenu("Test")]
    public void Test()
    {
        var comp = new ComputeBuffer(4, 4);
        Debug.Log($"Valid: {comp.IsValid()}");
        comp.Dispose();
    }
}
