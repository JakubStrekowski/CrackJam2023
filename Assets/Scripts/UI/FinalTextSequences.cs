using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum ENextFinalSceneAction
{
    ShowNextText,
    ShowThanks,
    GoToMenu
}

public class FinalTextSequences : MonoBehaviour
{
    [SerializeField] private TMP_Text text1A;
    [SerializeField] private TMP_Text text1B;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private CanvasGroup thanksPanel;

    [SerializeField] private float transitionTime = 0.75f;

    private ENextFinalSceneAction _nextAction;
    private bool _isLocked = false;

    public void ResolveNextClick()
    {
        switch (_nextAction)
        {
            default:
            case ENextFinalSceneAction.ShowNextText:
                NextText();
                break;
            case ENextFinalSceneAction.ShowThanks:
                ShowThanksPanel();
                break;
            case ENextFinalSceneAction.GoToMenu:
                GoToMenu();
                break;
        }
    }
    
    private void GoToMenu()
    {
        if (_isLocked) return;
        
        SceneManager.LoadScene(0);
    }
    
    private void ShowThanksPanel()
    {
        if (_isLocked) return;
        _isLocked = true;
        
        thanksPanel.gameObject.SetActive(true);
        thanksPanel.DOFade(1, transitionTime).onComplete = () =>
        {
            _isLocked = false;
            _nextAction = ENextFinalSceneAction.GoToMenu;
        };
    }
    
    private void NextText()
    {
        if (_isLocked) return;
        _isLocked = true;
        
        text2.gameObject.SetActive(true);
        DOTween.Sequence()
            .Append(text1A.DOColor(Color.clear, transitionTime))
            .Insert(0,text1B.DOColor(Color.clear, transitionTime))
            .Append(text2.DOColor(Color.cyan, transitionTime))
            .Play().onComplete = () =>
            {
                _isLocked = false;
                _nextAction = ENextFinalSceneAction.ShowThanks;
            };
    }
    private void Start()
    {
        _isLocked = true;
        
        DOTween.Sequence()
            .Append(text1A.DOColor(Color.cyan, transitionTime))
            .Append(text1B.DOColor(Color.cyan, transitionTime))
            .Play().onComplete = () =>
        {
            _isLocked = false;
            _nextAction = ENextFinalSceneAction.ShowNextText;
        };;
    }
}
