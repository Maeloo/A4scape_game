using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableCharacter : MonoBehaviour
{

    public Transform DialogBoxAnchor;
    public DialogueWrapper[] ActiveDialogue;

    protected int _currentDialogueIndex;

    protected bool bActive;


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

    public void StartInteracting()
    {
        if (bActive || ActiveDialogue.Length == 0)
        {
            return;
        }

        Vector3 DBScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(DialogBoxAnchor.position);

        _currentDialogueIndex = 0;

        UpdateDialogueBox();

        UIManager.Instance.DialogueBox.transform.position = DBScreenPosition;
        UIManager.Instance.DialogueBox.SetActive(true);

        bActive = true;
    }

    public void Interact()
    {
        if (bActive)
        {
            _currentDialogueIndex++;
            _currentDialogueIndex = Mathf.Min(_currentDialogueIndex, ActiveDialogue.Length);

            UpdateDialogueBox();
        }
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
