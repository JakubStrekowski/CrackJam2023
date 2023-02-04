using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicVolume;
    [SerializeField] private AudioMixerGroup soundsVolume;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;
    
    [SerializeField] private TMP_Text titleText;
    public void StartPlaying()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void Start()
    {
        musicSlider.value = 0f;
        OnMusicValueChange();
        soundsSlider.value = 0f;
        OnSoundsValueChange();

        StartCoroutine(nameof(BlinkPrompt));
    }

    public void OnMusicValueChange()
    {
        musicVolume.audioMixer.SetFloat("MusicVol", musicSlider.value);
    }
    public void OnSoundsValueChange()
    {
        soundsVolume.audioMixer.SetFloat("SoundsVol", soundsSlider.value);
    }

    private IEnumerator BlinkPrompt()
    {
        while (true)
        {
            titleText.text = ">r00t admin";
            yield return new WaitForSeconds(1);
            titleText.text = ">r00t admin_";
            yield return new WaitForSeconds(1);
        }
    }
}
