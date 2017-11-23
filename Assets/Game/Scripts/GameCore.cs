using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


	// Start Game
	void Start ()
    {
        m_player = Instantiate<GameObject>(PlayerPrefab).GetComponent<Character>();
        m_gameCamera = MainCamera.GetComponent<Camera>();

        _Kills = 0;

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

        BirdSpawner.Instance.Frequency = 10f;
        BirdSpawner.Instance.RandomDeviation = 5f;

        bA_Drunkard_1 = true;

        OnBirdKilled();
        OnBirdKilled();
        OnBirdKilled();
        OnBirdKilled();
        OnBirdKilled();
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
    }

    public void A_Doggo_1_Yes()
    {
        //Debug.Log("A_Doggo_1_Yes");
        GameCore.Instance.Player.StopInterracting();
        UIManager.Instance.FadeOut();
        Invoke("OnFadeTransition", 1.5f);
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
        }

        UIManager.Instance.KillsText.text = _Kills.ToString();
    }

    protected void OnFadeTransition()
    {
        UIManager.Instance.FadeIn();
    }

}
