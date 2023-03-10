using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Vector3[] Positions;

    public GameObject Target;

    private int _index;

    private void Awake()
    {
        Target.SetActive(false);
    }

    public void Next()
    {
        Target.transform.localPosition = Positions[_index++];
        Target.SetActive(true);
    }

    public void Select(int i)
    {
        _index = i;
        Next();
    }
}
