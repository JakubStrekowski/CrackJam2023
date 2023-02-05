using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayVictoryTransition : MonoBehaviour
{
    public void GoToVictory(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(2);
    }
}
