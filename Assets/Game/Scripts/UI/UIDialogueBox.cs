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


    public void UpateDialogue(string DialogueText, Texture SpritePortrait, string AnswerA = "", string AnswerB = "", Texture SpriteA = null, Texture SpriteB = null)
    {
        MainText.text = DialogueText;

        AnswerAText.text = AnswerA;
        AnswerBText.text = AnswerB;

        Portait.gameObject.SetActive(SpritePortrait != null);
        if (Portait.gameObject.activeSelf)
        {
            Portait.texture = SpritePortrait;
        }

        ImageA.gameObject.SetActive(SpriteA != null);
        if (ImageA.gameObject.activeSelf)
        {
            ImageA.texture = SpriteA;
        }
        
        ImageB.gameObject.SetActive(SpriteB != null);
        if (ImageB.gameObject.activeSelf)
        {
            ImageB.texture = SpriteB;
        }
    }

}
