using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class HackActivation : MonoBehaviour
{
    [SerializeField] private GameObject hackText;
    [SerializeField] private PlayableDirector director;
    private void OnTriggerEnter(Collider other)
    {
        hackText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        hackText.SetActive(false);
    }

    public void OnStartHack(InputAction.CallbackContext ctx )
    {
        hackText.SetActive(false);
        director.Play();
        this.enabled = false;
    }
}
