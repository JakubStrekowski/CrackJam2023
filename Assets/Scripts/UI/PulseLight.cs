using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PulseLight : MonoBehaviour
{
    [SerializeField] private Color originalColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private float transitionTime = 1.5f;
    
    private Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    void Start()
    {
        DOTween.Sequence()
            .Append(_img.DOColor(targetColor, transitionTime))
            .Append(_img.DOColor(originalColor, transitionTime))
            .SetLoops(-1)
            .Play();
    }
}
