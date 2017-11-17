using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableCharacter : MonoBehaviour
{

    public Transform DialogBoxAnchor;


	public void StartInteracting()
    {
        Vector3 DBScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(DialogBoxAnchor.position);

        UIManager.Instance.DialogueBox.transform.position = DBScreenPosition;
        UIManager.Instance.DialogueBox.SetActive(true);
    }

    public void StopInteracting()
    {
        UIManager.Instance.DialogueBox.SetActive(false);
    }

}
