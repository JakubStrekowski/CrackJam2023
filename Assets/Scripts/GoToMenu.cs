using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public void GoToMainMenu(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(0);
    }
}
