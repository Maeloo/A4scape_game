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
    }

    public void AnswerA()
    {
        Debug.Log("AnswerA");
    }

    public void AnswerB()
    {
        Debug.Log("AnswerB");
    }

    public void OnBirdKilled()
    {
        _Kills++;

        UIManager.Instance.KillsText.text = _Kills.ToString();
    }

}
