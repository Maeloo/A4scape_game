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

    [SerializeField]
    protected GameObject _ButtonIcon;

    [SerializeField]
    protected Texture[] PortraitSprites;


    private void Start()
    {
        _ButtonIcon.SetActive(false);
    }

    public void OnPlayerTriggered(bool bTriggered)
    {
        if(ActiveDialogue == null || (bTriggered && ActiveDialogue.Length == 0))
        {
            return;
        }

        _ButtonIcon.SetActive(bTriggered);
    }

    private void Update()
    {
        if (bActive && ActiveDialogue.Length > 0)
        {
            Vector3 DBScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(DialogBoxAnchor.position);
            UIManager.Instance.DialogueBox.transform.position = DBScreenPosition;

            if (ActiveDialogue[_currentDialogueIndex].bAnswer1 && Input.GetButtonDown(ActiveDialogue[_currentDialogueIndex].DialogueAnswers[0].AnswerInput))
            {
                GameCore.Instance.SendMessage(ActiveDialogue[_currentDialogueIndex].DialogueAnswers[0].AnswerCallback);
            }

            if (ActiveDialogue[_currentDialogueIndex].bAnswer2 && Input.GetButtonDown(ActiveDialogue[_currentDialogueIndex].DialogueAnswers[1].AnswerInput))
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

        RefreshDialogue();

        UIManager.Instance.DisplayDialogue(true);

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
            PortraitSprites,
            ActiveDialogue[_currentDialogueIndex].DialogueAnswers[0].AnswerText,
            ActiveDialogue[_currentDialogueIndex].DialogueAnswers[1].AnswerText,
            ActiveDialogue[_currentDialogueIndex].bAnswer1,
            ActiveDialogue[_currentDialogueIndex].bAnswer2,
            ActiveDialogue[_currentDialogueIndex].bNext
            );
    }

    public void StopInteracting()
    {
        UIManager.Instance.DisplayDialogue(false);

        Invoke("OnStoppedInterracting", .5f);
        
    }

    void OnStoppedInterracting()
    {
        bActive = false;
    }

}
