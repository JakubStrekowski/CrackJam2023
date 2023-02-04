using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum EMoveDirection
{   
    Left,
    Right
}

public class LaserEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] private float moveSpeed = 0.25f;
    [SerializeField] private float timeStopBetweenDirections = 1.4f;
    [SerializeField] private float lifeTime = 6f;
    [SerializeField] private EMoveDirection moveDirection;
    
    private bool isMovementStarted = false;
    private Transform _transform;
    
    public override void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        StartCoroutine(nameof(AwaitForMovement));
        StartCoroutine(nameof(LifeTimeSet));
        StartCoroutine(nameof(StartShooting));
        OnSpawn();
    }

    private void FixedUpdate()
    {
        if (!isMovementStarted) return;
        
        switch (moveDirection)
        {
            default:
            case EMoveDirection.Left:
                _transform.position += (_transform.rotation * Vector3.left) * moveSpeed;
                break;
            case EMoveDirection.Right:
                _transform.position += (_transform.rotation * Vector3.right) * moveSpeed;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (moveDirection == EMoveDirection.Left) moveDirection = EMoveDirection.Right;
        else if(moveDirection == EMoveDirection.Right) moveDirection = EMoveDirection.Left;
        StartCoroutine(nameof(AwaitForMovement));
    }

    private IEnumerator AwaitForMovement()
    {
        isMovementStarted = false;
        yield return new WaitForSeconds(timeStopBetweenDirections);
        isMovementStarted = true;
    }
    
    private IEnumerator LifeTimeSet()
    {
        yield return new WaitForSeconds(lifeTime);
        OnDespawn();
    }
    private IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(timeStopBetweenDirections);
        OnAttack();
    }
}
