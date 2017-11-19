using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnswerWrapper
{
    public string AnswerText;
    public Texture AnswerSprite;
    public KeyCode AnswerInput;
    public string AnswerCallback;
};

[System.Serializable]
public class DialogueWrapper
{

    public string DialogueText;
    public AnswerWrapper[] DialogueAnswers = new AnswerWrapper[2];
	
}
