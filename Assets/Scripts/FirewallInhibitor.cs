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
    
    private float _currentCountdown;

    private Sequence _sequence;
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
    }

    public void OnDisable()
    {
        StopCoroutine(nameof(CountDown));
        _sequence.Pause();
        compromiseSlider.gameObject.SetActive(false);
    }

    private IEnumerator CountDown()
    {
        while (_currentCountdown > 0)
        {
            _currentCountdown -= Time.deltaTime;
            compromiseSlider.value = _currentCountdown / timeToCompromise;
            yield return null;
        }
        
        onCompromised?.Invoke();
        yield return null;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onEnteredOnTime?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onEnteredOnTime?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
