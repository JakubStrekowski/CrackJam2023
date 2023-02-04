using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent OnShoot;
    public virtual void OnSpawn()
    {
        Debug.Log("Virtual spawn effect");
    }
    public virtual void OnDespawn()
    {
        Debug.Log("Virtual despawn effect");
    }
    public virtual void OnAttack()
    {
        Debug.Log("Virtual onShoot");
        OnShoot?.Invoke();
    }
}
