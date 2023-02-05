using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RestartDecider", order = 1)]
public class RestartDecider : ScriptableObject
{
    public bool isTutorialDeath = false;

    public void EnableTutorialDeath()
    {
        isTutorialDeath = true;
    }
}
