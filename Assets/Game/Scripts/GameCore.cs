using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameCore : Singleton<GameCore>
{
    [SerializeField]
    protected GameObject PlayerPrefab;

    [SerializeField]
    protected FollowCamera MainCamera;

    public Camera GameCamera { get { return m_gameCamera; } }
    public Camera m_gameCamera;

    private Character m_player;
    public Character Player { get { return m_player; } }

    protected int _Kills;

    public float PopupAutoTimer = 30f;
    public float PopupCooldown;

    protected GameObject[] t1_elements;
    protected GameObject[] t2_elements;

    protected bool bPaused;


    // Start Game
    void Start ()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        m_player = Instantiate<GameObject>(PlayerPrefab).GetComponent<Character>();
        m_gameCamera = MainCamera.GetComponent<Camera>();

        _Kills = 0;

        t1_elements = GameObject.FindGameObjectsWithTag("T1");
        t2_elements = GameObject.FindGameObjectsWithTag("T2");

        foreach (GameObject el2 in t2_elements)
        {
            el2.SetActive(false);
        }

        GameObject[] landlords = GameObject.FindGameObjectsWithTag("Landlord");
        foreach (GameObject ll in landlords)
        {
            ll.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
            {
                DialogueData.Landlord_1_a,
                DialogueData.Landlord_1_b,
                DialogueData.Landlord_1_c,
                DialogueData.Landlord_1_d,
            });
        }

        UIManager.Instance.ObjectiveText.text = DialogueData.Objective_1;
        PopupCooldown = 0f;
    }

    public void TogglePause()
    {
        bPaused = !bPaused;
        Time.timeScale = bPaused ? 0f : 1f;
    }

    private void Update()
    {
        PopupCooldown -= Time.deltaTime;
        if(PopupCooldown <=  0f)
        {
            PopupCooldown = PopupAutoTimer;
            UIManager.Instance.DisplayPopup(true);
        }
    }

    public void A_Landlord_1_Yes()
    {
        //Debug.Log("A_Landlord_1_Yes");
        GameObject[] landlords = GameObject.FindGameObjectsWithTag("Landlord");
        foreach (GameObject ll in landlords)
        {
            ll.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
            {
                DialogueData.Landlord_2_a,
                DialogueData.Landlord_2_b
            });
            ll.GetComponent<InteractableCharacter>().RefreshDialogue();
        }

        GameObject[] drunkards = GameObject.FindGameObjectsWithTag("Drunkard");
        foreach (GameObject d in drunkards)
        {
            d.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
            {
                DialogueData.Drunkard_1_a,
                DialogueData.Drunkard_1_b,
                DialogueData.Drunkard_1_c
            });
        }

        UIManager.Instance.ObjectiveText.text = DialogueData.Objective_2;
        PopupCooldown = 0f;
    }

    public void A_Landlord_1_No()
    {
        //Debug.Log("A_Landlord_1_No");
        GameCore.Instance.Player.StopInterracting();
    }

    bool bA_Drunkard_1 = false;
    public void A_Drunkard_1()
    {
        if (bA_Drunkard_1)
            return;

        //Debug.Log("A_Drunkard_1");
        InteractableItem[] beers = FindObjectsOfType<InteractableItem>();
        foreach (InteractableItem beer in beers)
        {
            beer.ToggleActive();
        }

        GameCore.Instance.Player.StopInterracting();

        BirdSpawner.Instance.Frequency = 8f;
        BirdSpawner.Instance.RandomDeviation = 3f;

        bA_Drunkard_1 = true;

        UIManager.Instance.DisplayBirdCount(true);
        UIManager.Instance.ObjectiveText.text = DialogueData.Objective_3;
        PopupCooldown = 0f;

        //OnBirdKilled();
        //OnBirdKilled();
        //OnBirdKilled();
        //OnBirdKilled();
        //OnBirdKilled();
    }

    public void A_Landlord_3()
    {
        //Debug.Log("A_Landlord_3");
        GameObject[] doggos = GameObject.FindGameObjectsWithTag("Doggo");
        foreach (GameObject d in doggos)
        {
            d.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
            {
                DialogueData.Doggo_3_a,
                DialogueData.Doggo_3_b,
                DialogueData.Doggo_3_c
            });
        }

        UIManager.Instance.ObjectiveText.text = DialogueData.Objective_6;
        PopupCooldown = 0f;
    }

    public void A_Doggo_1_Yes()
    {
        //Debug.Log("A_Doggo_1_Yes");
        GameCore.Instance.Player.StopInterracting();
        GameCore.Instance.Player.CharacterController.SetIgnoreInput(true);
        GameCore.Instance.Player.CharacterController.SetIgnoreMove(true);

        UIManager.Instance.FadeOut();

        UIManager.Instance.DisplayBeerCount(false);
        UIManager.Instance.DisplayBirdCount(false);

        Invoke("OnFadeTransition", 2f);
    }

    public void A_Doggo_1_No()
    {
        //Debug.Log("A_Doggo_1_No");
        GameCore.Instance.Player.StopInterracting();
    }

    public void OnBirdKilled()
    {
        _Kills++;

        if (_Kills == 5)
        {
            GameObject[] landlords = GameObject.FindGameObjectsWithTag("Landlord");
            foreach (GameObject ll in landlords)
            {
                ll.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
                {
                DialogueData.Landlord_3_a,
                DialogueData.Landlord_3_b,
                DialogueData.Landlord_3_c
                });
            }

            UIManager.Instance.ObjectiveText.text = DialogueData.Objective_5;
            PopupCooldown = 0f;
        }

        UIManager.Instance.KillsText.text = _Kills.ToString();
    }

    protected void OnFadeTransition()
    {
        foreach (GameObject el1 in t1_elements)
        {
            el1.SetActive(false);
        }

        foreach (GameObject el2 in t2_elements)
        {
            el2.SetActive(true);
        }

        UIManager.Instance.FadeIn();
        UIManager.Instance.ObjectiveText.text = DialogueData.Objective_7;
        PopupCooldown = 0f;

        GameCore.Instance.Player.CharacterController.ResetIgnoreInput();
        GameCore.Instance.Player.CharacterController.ResetIgnoreMove();
    }

}
