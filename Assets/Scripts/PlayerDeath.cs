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
    public UnityEvent OnPlayerTutorialDeath;
    public GameObject deathParticles;
    [SerializeField] private RestartDecider restartData;
    private Vector3 _startPos;
    
    private bool isDead = false;
    private bool deathLock = false;

    public float ImmuneTime;
    private float _lastImmuneTime = Single.MinValue;

    private void Start()
    {
        _startPos = transform.position;
    }

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
        if (restartData.isTutorialDeath)
        {
            if (!deathLock)
            {
                StartCoroutine(nameof(DeathLocked));
            }
        }
        else
        {
            isDead = true;
            Instantiate(deathParticles, transform.position, quaternion.identity);
            OnPlayerDeath?.Invoke();
        }
    }

    private IEnumerator DeathLocked()
    {
        deathLock = true;
        Instantiate(deathParticles, transform.position, quaternion.identity);
        transform.position = _startPos;
        OnPlayerTutorialDeath?.Invoke();
        yield return new WaitForSeconds(0.2f);
        deathLock = false;
    }
}
