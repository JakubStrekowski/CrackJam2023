using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public UnityEvent OnPlayerDeath;
    public GameObject deathParticles;
    
    private bool isDead = false;

    public float ImmuneTime;
    private float _lastImmuneTime = Single.MinValue;

    public void Immune()
    {
        _lastImmuneTime = Time.time;
    }

    public bool IsImmune() => Time.time < _lastImmuneTime + ImmuneTime;

    public void TrySetDeath()
    {
        if (IsImmune()) return;
        SetDeath();
    }

    public void SetDeath()
    {
        isDead = true;
        Instantiate(deathParticles, transform.position, quaternion.identity);
        OnPlayerDeath?.Invoke();
    }
}
