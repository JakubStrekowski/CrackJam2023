using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedModifier = 0.4f;

    private Transform _transform;
    private Vector3 _currentMovement;

    public Animator Animator;
    public Rigidbody Body;

    public void Movement ( InputAction.CallbackContext ctx )
    {
        var cameraTransform = Camera.main.transform;
        Vector2 readValue = ctx.ReadValue<Vector2>() * speedModifier;
        var forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized * readValue.y;
        var side = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized * readValue.x;
        _currentMovement = Vector3.ClampMagnitude(forward + side, 1f);
    }
    
    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_currentMovement != Vector3.zero)
        {
            Body.velocity = _currentMovement * speedModifier;
            // _transform.position = _transform.position + (_currentMovement * speedModifier * Time.fixedDeltaTime) ;
            _transform.LookAt(transform.position + _currentMovement, Vector3.up);
            Animator.SetBool("IsRunning", true);
        }
        else
        {
            Animator.SetBool("IsRunning", false);
            Body.velocity = Vector3.zero;
        }
    }
}
