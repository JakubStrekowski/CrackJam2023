using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BoxObstacle : MonoBehaviour
{
    [SerializeField] private UnityEvent onSpawned;
    [SerializeField] private float timeToRise = 1f;
    [SerializeField] private float targetYPos = 0.5f;
    [SerializeField] private float backYPos = -0.6f;
    [SerializeField] private Ease riseEase;
    [SerializeField] private Ease hideEase;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }
    
    [ContextMenu("Disable")]
    public void SetDisable()
    {
        _transform.DOMoveY(backYPos, timeToRise).SetEase(hideEase)
            .onComplete = () => {gameObject.SetActive(false);};
    }

    private void OnEnable()
    {
        onSpawned?.Invoke();
        _transform.position = new Vector3(_transform.position.x, backYPos, _transform.position.z);

        _transform.DOMoveY(targetYPos, timeToRise).SetEase(riseEase);
    }

}
