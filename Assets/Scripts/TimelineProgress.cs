using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimelineProgress : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text victoryText;

    private PlayableDirector _director;
    private bool _isLocked;

    public void LockHackProgress()
    {
        _isLocked = true;
    }
    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (_isLocked) return;
        
        if (slider.IsActive())
        {
            slider.value = (float)(_director.time / _director.duration);
        }

        if (slider.value > 0.99f)
        {
            victoryText.gameObject.SetActive(true);
        }
    }
}
