using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public UnityEvent OnPlayerDeath;
    
    private bool isDead = false;

    public void Restart ( InputAction.CallbackContext ctx )
    {
        if (!isDead) return;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetDeath()
    {
        isDead = true;
        OnPlayerDeath?.Invoke();
    }
}
