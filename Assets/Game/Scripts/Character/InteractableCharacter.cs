using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableCharacter : MonoBehaviour
{

    public Transform DialogBoxAnchor;

    protected DialogueWrapper[] ActiveDialogue;

    protected int _currentDialogueIndex;

    protected bool bActive;
    protected bool bHasDialogue;


    private void Update()
    {
        if (bActive && ActiveDialogue.Length > 0)
        {
            if (Input.GetKeyDown(ActiveDialogue[_currentDialogueIndex].DialogueAnswers[0].AnswerInput))
            {
                GameCore.Instance.SendMessage(ActiveDialogue[_currentDialogueIndex].DialogueAnswers[0].AnswerCallback);
            }

            if (Input.GetKeyDown(ActiveDialogue[_currentDialogueIndex].DialogueAnswers[1].AnswerInput))
            {
                GameCore.Instance.SendMessage(ActiveDialogue[_currentDialogueIndex].DialogueAnswers[1].AnswerCallback);
            }
        }
    }

    public void SetDialogue(DialogueWrapper[] NewDialogue)
    {
        ActiveDialogue = NewDialogue;
        bHasDialogue = ActiveDialogue.Length > 0;
    }

    public void StartInteracting()
    {
        if (bActive || ActiveDialogue == null || ActiveDialogue.Length == 0)
        {
            return;
        }

        Vector3 DBScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(DialogBoxAnchor.position);

        RefreshDialogue();

        UIManager.Instance.DialogueBox.transform.position = DBScreenPosition;
        UIManager.Instance.DialogueBox.SetActive(true);

        bActive = true;
    }

    public void Interact()
    {
        if (bActive)
        {
            _currentDialogueIndex++;
            _currentDialogueIndex = Mathf.Min(_currentDialogueIndex, ActiveDialogue.Length - 1);

            UpdateDialogueBox();
        }
    }

    public void RefreshDialogue()
    {
        _currentDialogueIndex = 0;

        UpdateDialogueBox();
    }

    protected void UpdateDialogueBox()
    {
        UIManager.Instance.Dialogue.UpateDialogue(
            ActiveDialogue[_currentDialogueIndex].DialogueText,
            ActiveDialogue[_currentDialogueIndex].DialogueAnswers[0].AnswerText,
            ActiveDialogue[_currentDialogueIndex].DialogueAnswers[1].AnswerText,
            ActiveDialogue[_currentDialogueIndex].DialogueAnswers[0].AnswerSprite,
            ActiveDialogue[_currentDialogueIndex].DialogueAnswers[1].AnswerSprite
            );
    }

    public void StopInteracting()
    {
        UIManager.Instance.DialogueBox.SetActive(false);

        bActive = false;
    }

}
