﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    protected GameObject _DialogueBox;
    public GameObject DialogueBox { get { return _DialogueBox; } }
    public UIDialogueBox Dialogue { get { return _DialogueBox.GetComponent<UIDialogueBox>(); } }

    [SerializeField]
    protected Text _DebugText;
    public Text DebugText { get { return _DebugText; } }

    [SerializeField]
    protected Text _KillsText;
    public Text KillsText { get { return _KillsText; } }

    [SerializeField]
    protected Text _BeersText;
    public Text BeersText { get { return _BeersText; } }

    [SerializeField]
    protected RawImage FadeInOut;

    [SerializeField]
    protected RectTransform _Popup;
    public Text ObjectiveText { get { return _Popup.GetComponentInChildren<Text>(); } }

    private void Start()
    {
        DisplayBeerCount(false);
        DisplayBirdCount(false);
        //DisplayPopup(false);

        Invoke("OnStart", 1f);
    }

    void OnStart()
    {
        FadeInOut.CrossFadeColor(new Color(0f, 0f, 0f, 0f), 1f, true, true);
    }

    public void FadeOut()
    {
        FadeInOut.CrossFadeColor(new Color(0f, 0f, 0f, 1f), 1f, true, true);
    }

    public void FadeIn()
    {
        FadeInOut.CrossFadeColor(new Color(0f, 0f, 0f, 0f), 1f, true, true);
    }

    public void DisplayBeerCount(bool bDisplay)
    {
        RectTransform beerContainer = BeersText.rectTransform;
        beerContainer.DOAnchorPosX(!bDisplay ? 2130f : 1830f, 2f).SetEase(Ease.InOutCubic);
    }

    public void DisplayBirdCount(bool bDisplay)
    {
        RectTransform birdContainer = KillsText.rectTransform;
        birdContainer.DOAnchorPosX(!bDisplay ? 2130f : 1830f, 2f).SetEase(Ease.InOutCubic);
    }

    public void DisplayPopup(bool bDisplay)
    {
        CancelInvoke("HidePopup");

        _Popup.DOKill();
        _Popup.DOAnchorPosY(bDisplay ? 0f : 300f, 2f).SetEase(Ease.InOutCubic);

        Invoke("HidePopup", 5f);
    }

    protected void HidePopup()
    {
        DisplayPopup(false);
    }

}
