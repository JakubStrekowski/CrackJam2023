using System;
using System.Collections;
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
    [SerializeField] private float directorDelay = 1.5f;
    [SerializeField] private LaserBeam laserToDisable;
    [SerializeField] private RestartDecider restartDecider;
    public GameObject TutorialTarget;

    public bool SkipTutorial;

    private Collider col;
    private bool _wasActivated;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        col.enabled = false;

        #if UNITY_EDITOR
        if (SkipTutorial)
        {
            hackText.SetActive(true);
            OnStartHack(default);

            var pos = transform.position;
            pos.y = 0;
            FindObjectOfType<PlayerMovement>().transform.position = pos;
        }
        #endif
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
        if (hackText == null) return;
        if (!hackText.activeInHierarchy) return;

        particleSystemRoot.SetActive(false);
        hackText.SetActive(false);
        _wasActivated = true;
        slider.gameObject.SetActive(true);
        StartCoroutine(nameof(DirectorDelay));
        laserToDisable.ConstantShooting = false;
        restartDecider.isTutorialDeath = false;

        transform.DOMoveY(-1.5f, 2);
        col.enabled = false;
        
        TutorialTarget.SetActive(false);
    }

    public void LiftTheRoot()
    {
        transform.DOMoveY(0.5f, 1);
        particleSystemRoot.SetActive(true);
        col.enabled = true;
    }

    private IEnumerator DirectorDelay()
    {
        if(!SkipTutorial)
        yield return new WaitForSeconds(directorDelay);
        director.Play();
    }
}
