using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeAfterTime : MonoBehaviour
{
    [SerializeField] private float timeUntilFade = 5;
    [SerializeField] private float fadeDuration = 0.4f;

    private TMP_Text _text; 

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        StartCoroutine(nameof(WaitAndFade));
    }

    private IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(timeUntilFade);

        _text.DOColor(Color.clear, fadeDuration);
    }
}
