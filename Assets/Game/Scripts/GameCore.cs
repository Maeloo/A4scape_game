using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GameCore : Singleton<GameCore>
{
    [SerializeField]
    protected GameObject PlayerPrefab;

    [SerializeField]
    protected FollowCamera MainCamera;

    [SerializeField]
    protected Transform EndBackground;

    [SerializeField]
    protected AudioClip MusicLevel2;

    public Camera GameCamera { get { return m_gameCamera; } }
    public Camera m_gameCamera;

    private Character m_player;
    public Character Player { get { return m_player; } }

    protected AudioSource Music;

    protected int _Kills;

    public float PopupAutoTimer = 30f;
    public float PopupCooldown;

    protected GameObject[] t1_elements;
    protected GameObject[] t2_elements;
    protected GameObject[] landlords_futur;

    protected bool bPaused;
    public bool GamePaused { get { return bPaused; } }


    // Start Game
    void Start ()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        Cursor.visible = false;

        Music = GetComponent<AudioSource>();

        MeteorSpawner.Instance.gameObject.SetActive(false);

        m_player = Instantiate<GameObject>(PlayerPrefab).GetComponent<Character>();
        m_gameCamera = MainCamera.GetComponent<Camera>();

        _Kills = 0;

        t1_elements = GameObject.FindGameObjectsWithTag("T1");
        t2_elements = GameObject.FindGameObjectsWithTag("T2");
        landlords_futur = GameObject.FindGameObjectsWithTag("Landlord_evil");

        foreach (GameObject el2 in t2_elements)
        {
            el2.SetActive(false);
        }

        foreach (GameObject ll in landlords_futur)
        {
            ll.SetActive(false);
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

        UIManager.Instance.DisplayPause(bPaused);

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

    public void CloseDialogue()
    {
        Player.StopInterracting();
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

        BirdSpawner.Instance.Frequency = 6f;
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

    int hack_fix = 0;
    public void A_Landlord_3_PMT()
    {
        hack_fix++;
        if (hack_fix == 1) return;
        //Debug.Log("A_Landlord_PMT");
        GameObject[] doggos = GameObject.FindGameObjectsWithTag("Doggo");
        foreach (GameObject d in doggos)
        {
            d.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
            {
                DialogueData.Doggo_3_a,
                DialogueData.Doggo_3_b,
                DialogueData.Doggo_3_c,
                DialogueData.Doggo_3_d
            });
        }

        Player.StopInterracting();

        UIManager.Instance.ObjectiveText.text = DialogueData.Objective_6;
        PopupCooldown = 0f;
    }

    public void A_Doggo_1_Yes()
    {
        //Debug.Log("A_Doggo_1_Yes");
        Player.StopInterracting();
        Player.CharacterController.SetIgnoreInput(true);
        Player.CharacterController.SetIgnoreMove(true);

        UIManager.Instance.FadeOut();

        UIManager.Instance.DisplayBeerCount(false);
        UIManager.Instance.DisplayBirdCount(false);

        Music.DOFade(0f, 2f).OnComplete(() => 
        {
            Music.Stop();
            Music.clip = MusicLevel2;
            Music.Play();
        });
        Music.DOFade(.3f, 2f).SetDelay(2f);

        Invoke("OnFadeTransition", 2f);
    }

    public void OnBirdKilled()
    {
        _Kills++;

        if (_Kills == 5)
        {
            GameObject[] landlords = GameObject.FindGameObjectsWithTag("Landlord");
            foreach (GameObject ll in landlords)
            {
                ll.SetActive(false);
            }

            foreach (GameObject llf in landlords_futur)
            {
                llf.SetActive(true);
                llf.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
                {
                    DialogueData.Landlord_3_a,
                    DialogueData.Landlord_3_b,
                    DialogueData.Landlord_3_c,
                    DialogueData.Landlord_3_d,
                    DialogueData.Landlord_3_e
                });
            }

            UIManager.Instance.ObjectiveText.text = DialogueData.Objective_5;
            PopupCooldown = 0f;
        }

        UIManager.Instance.KillsText.text = _Kills.ToString();
    }

    protected void OnFadeTransition()
    {
        Player.bBlockLeftMovement = true;
        Player.LeftBorderDistance = 50f;   

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

        GameObject[] doggos = GameObject.FindGameObjectsWithTag("Doggo_Futur");
        foreach (GameObject d in doggos)
        {
            d.GetComponent<InteractableCharacter>().SetDialogue(new DialogueWrapper[]
            {
                DialogueData.DoggoFutur_3_a,
                DialogueData.DoggoFutur_3_b,
                DialogueData.DoggoFutur_3_c,
                DialogueData.DoggoFutur_3_d,
                DialogueData.DoggoFutur_3_e,
                DialogueData.DoggoFutur_3_f,
                DialogueData.DoggoFutur_3_g,
                DialogueData.DoggoFutur_3_h,
                DialogueData.DoggoFutur_3_i
            });
        }

        GameCore.Instance.Player.CharacterController.ResetIgnoreInput();
        GameCore.Instance.Player.CharacterController.ResetIgnoreMove();

        MeteorSpawner.Instance.gameObject.SetActive(true);
    }

    public void PreEnd()
    {
        Player.CharacterController.SetIgnoreMove(true);

        MeteorSpawner.Instance.Stop();

        UIManager.Instance.DisplayPopup(false);

        EndBackground.DOScaleX(8f, 1f);
        EndBackground.DOScaleY(16f, 1f);

        Player.Renderer.sortingOrder = 100;

        GameObject[] doggo_int = GameObject.FindGameObjectsWithTag("Doggo_Futur");
        GameObject[] doggo_noi = GameObject.FindGameObjectsWithTag("Doggo_Simple");

        List<GameObject> doggos = new List<GameObject>();
        doggos.AddRange(doggo_int);
        doggos.AddRange(doggo_noi);

        foreach (GameObject d in doggos)
        {
            d.GetComponent<SpriteRenderer>().sortingOrder = 99;
        }
    }

    public bool bEnded;
    int hack_fix_bis = 0;
    public void A_DoggoFutur_1()
    {
        hack_fix_bis++;
        if (hack_fix_bis == 1) return;

        MeteorSpawner.Instance.gameObject.SetActive(false);

        Player.StopInterracting();
        Player.CharacterController.SetIgnoreInput(true);

        UIManager.Instance.FadeOut();
        UIManager.Instance.DisplayEndContainer(true, 4f, 1f);

        GetComponent<AudioSource>().DOFade(0f, 4f);

        UIManager.Instance.EndMusic.Play();
        UIManager.Instance.EndMusic.DOFade(.3f, 4f);

        bEnded = true;
    }

}
