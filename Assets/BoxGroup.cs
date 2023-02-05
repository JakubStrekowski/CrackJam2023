using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGroup : MonoBehaviour
{
    public bool ActiveBoxes;
    private bool _lastValue;
    private BoxObstacle[] _boxes;

    private void Awake()
    {
        _boxes = GetComponentsInChildren<BoxObstacle>(true);
    }

    private void FixedUpdate()
    {
        if (ActiveBoxes != _lastValue)
        {
            _lastValue = ActiveBoxes;
            if (ActiveBoxes)
            {
                foreach (var box in _boxes)
                {
                    box.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (var box in _boxes)
                {
                    box.SetDisable();
                }
            }
        }
    }

    public void Show()
    {
        ActiveBoxes = true;
    }

    public void Hide()
    {
        ActiveBoxes = false;
    }
}
