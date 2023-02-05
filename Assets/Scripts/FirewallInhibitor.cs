using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FirewallInhibitor : MonoBehaviour
{
    [SerializeField] private Slider compromiseSlider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private float timeToCompromise = 6f;
    [SerializeField] private UnityEvent onSpawned;
    [SerializeField] private UnityEvent onCompromised;
    [SerializeField] private UnityEvent onEnteredOnTime;
    [SerializeField] private PlayerDeath playerDeath;
    [SerializeField] private AudioClip enterClip;

    public GameObject SuccessParticles;

    public float ColliderActivationTime;
    public Collider Collider;
    
    private float _currentCountdown;

    private Sequence _sequence;

    private void Start()
    {
        if (playerDeath != null)
        {
            playerDeath.OnPlayerTutorialDeath.AddListener(() =>
            {
                gameObject.SetActive(false);
                gameObject.SetActive(true);
            });
        }
    }

    public void OnEnable()
    {
        _currentCountdown = timeToCompromise;
        compromiseSlider.gameObject.SetActive(true);
        onSpawned?.Invoke();
        
        _sequence = DOTween.Sequence()
            .Append(sliderFill.DOColor(new Color(1f, 0.4231346f, 0.3921568f), 0.4f))
            .Append(sliderFill.DOColor(new Color(1f, 0.4231346f, 0.3921568f, 0.25f), 0.4f))
            .Append(sliderFill.DOColor(new Color(1f, 0.4231346f, 0.3921568f), 0.4f))
            .SetLoops(-1)
            .Play();
        
        StopCoroutine(nameof(CountDown));
        StartCoroutine(nameof(CountDown));
        
        StopCoroutine(nameof(ColliderController));
        StartCoroutine(nameof(ColliderController));
    }

    public IEnumerator ColliderController()
    {
        Collider.enabled = false;
        yield return new WaitForSeconds(ColliderActivationTime);
        Collider.enabled = true;
    }

    public void OnDisable()
    {
        StopCoroutine(nameof(CountDown));
        _sequence.Pause();
        if (compromiseSlider != null)
        {
            compromiseSlider.gameObject.SetActive(false);
        }
    }

    private IEnumerator CountDown()
    {
        while (_currentCountdown > 0)
        {
            _currentCountdown -= Time.deltaTime;
            compromiseSlider.value = _currentCountdown / timeToCompromise;
            yield return null;
        }

        if (HasPlayer)
        {
            Success();
        }
        else
        {
            Fail();
        }
        yield return null;
    }

    private void FixedUpdate()
    {
        if (HasPlayer)
        {
            Load += Time.fixedDeltaTime;
            if (Load >= LoadTime)
            {
                Success();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onEnteredOnTime?.Invoke();
            AudioSource.PlayClipAtPoint(enterClip, transform.position);
            gameObject.SetActive(false);
        }
    }

    public float LoadTime;
    private bool HasPlayer;
    private float Load;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HasPlayer = false;
            Load = 0;
        }
    }

    private void Success()
    {
        onEnteredOnTime?.Invoke();
        gameObject.SetActive(false);
        HasPlayer = false;
        Load = 0f;

        SuccessParticles.transform.position = transform.position;
        SuccessParticles.SetActive(true);
    }

    private void Fail()
    {
        onCompromised?.Invoke();
    }
}
