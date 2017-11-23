﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

}