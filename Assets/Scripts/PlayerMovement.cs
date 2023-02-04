using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedModifier = 0.4f;

    private Transform _transform;
    private Vector3 _currentMovement;

    public void Movement ( InputAction.CallbackContext ctx )
    {
        Vector2 readValue = ctx.ReadValue<Vector2>() * speedModifier;
        _currentMovement = new Vector3(readValue.x, 0, readValue.y);
    }
    
    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_currentMovement != Vector3.zero)
        {
            _transform.position = _transform.position + (transform.rotation * _currentMovement * speedModifier) ;
        }
    }
}
