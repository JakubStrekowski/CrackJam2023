using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class HackActivation : MonoBehaviour
{
    [SerializeField] private GameObject hackText;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject particleSystemRoot;

    private Collider col;
    private bool _wasActivated;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_wasActivated) return;
        
        hackText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        hackText.SetActive(false);
    }

    public void OnStartHack(InputAction.CallbackContext ctx )
    {
        if (_wasActivated) return;
        if (!hackText.activeInHierarchy) return;

        particleSystemRoot.SetActive(false);
        hackText.SetActive(false);
        director.Play();
        _wasActivated = true;
        slider.gameObject.SetActive(true);

        transform.DOMoveY(-1.5f, 2);
        col.enabled = false;
    }
}
