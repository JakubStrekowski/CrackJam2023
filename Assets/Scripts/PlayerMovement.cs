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

    public float DashDistance;
    public float DashCooldown;
    public LayerMask DashMask;
    public SphereCollider DashCollider;
    private float _lastDashTime = float.MinValue;

    public TrailRenderer[] DashTrails;
    public float DTTime;
    public float DTFade;

    public AudioSource DashAudioSource;
#if UNITY_EDITOR
    public float MoreSpeed = 2f;

    private void Update()
    {
        Time.timeScale = Keyboard.current.leftShiftKey.isPressed ? MoreSpeed : 1f;

        if (Keyboard.current.leftCommandKey.wasPressedThisFrame)
        {
            Debug.Break();
        }
    }
#endif
    public void Movement ( InputAction.CallbackContext ctx )
    {
        var cameraTransform = Camera.main.transform;
        Vector2 readValue = ctx.ReadValue<Vector2>() * speedModifier;
        var forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized * readValue.y;
        var side = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized * readValue.x;
        _currentMovement = Vector3.ClampMagnitude(forward + side, 1f);
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (_currentMovement != Vector3.zero && Time.time > _lastDashTime + DashCooldown)
        {
            _lastDashTime = Time.time;

            foreach (var trail in DashTrails)
            {
                trail.time = DTTime;
                trail.DOTime(0f, DTFade);
            }

            var position = DashCollider.transform.position;
            var radius = DashCollider.radius;
            var hasHit = Physics.SphereCast(position, radius, _currentMovement.normalized, out RaycastHit hit,
                DashDistance, DashMask);
            var distance = hasHit ? hit.distance - radius : DashDistance;

            Body.position += _currentMovement.normalized * distance;

            DashAudioSource.panStereo = _currentMovement.x;
            DashAudioSource.Play();
            
            GetComponent<PlayerDeath>().Immune();
        }
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
