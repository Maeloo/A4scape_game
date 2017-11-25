using System.Collections;
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

    private void Start()
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
        Transform beerContainer = BeersText.transform.parent;
        beerContainer.DOMoveX(bDisplay ? -90f : 90f, 1f).SetEase(Ease.InOutExpo);
    }

}
