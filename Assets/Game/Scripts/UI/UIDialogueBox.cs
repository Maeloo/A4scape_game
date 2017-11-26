using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueBox : MonoBehaviour
{

    [SerializeField]
    protected Text MainText;
    [SerializeField]
    protected Text AnswerAText;
    [SerializeField]
    protected Text AnswerBText;
    [SerializeField]
    protected RawImage ImageA;
    [SerializeField]
    protected RawImage ImageB;
    [SerializeField]
    protected RawImage Portait;
    [SerializeField]
    protected GameObject NextObj;

    protected Texture[] _animPortrait;
    protected int _currentPortrait;
    protected float _cooldownPortrait;
    public float _ratePortrait = 2f;


    public void UpateDialogue(string DialogueText, Texture[] SpritePortraits, string AnswerA, string AnswerB, bool bAnswerA, bool bAnswerB, bool bNext)
    {
        MainText.text = DialogueText;

        AnswerAText.text = AnswerA;
        AnswerBText.text = AnswerB;

        _animPortrait = SpritePortraits;
        _currentPortrait = 0;
        _cooldownPortrait = 1f / _ratePortrait;

        Portait.texture = _animPortrait[_currentPortrait];

        ImageA.gameObject.SetActive(bAnswerA);        
        ImageB.gameObject.SetActive(bAnswerB);

        NextObj.SetActive(bNext);
    }

    private void Update()
    {
        _cooldownPortrait -= Time.deltaTime;
        if (_cooldownPortrait <= 0f)
        {
            _cooldownPortrait = 1f / _ratePortrait;
            _currentPortrait = _currentPortrait + 1 >= _animPortrait.Length ? 0 : _currentPortrait + 1;

            Portait.texture = _animPortrait[_currentPortrait];
        }
    }

}
