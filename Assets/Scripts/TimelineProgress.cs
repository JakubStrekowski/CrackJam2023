using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimelineProgress : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private PlayableDirector _director;
    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        _director.stopped += OnPlayableDirectorStopped;
    }
    
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        slider.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (slider.IsActive())
        {
            slider.value = (float)(_director.time / _director.duration);
        }
    }
}
