using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class UIHome : MonoBehaviour
{

    [SerializeField]
    protected CanvasGroup Container;

    protected bool bStarted = false;

	void Update ()
    {
	    if (!bStarted && Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A"))
        {
            bStarted = true;

            Container.DOFade(0f, .6f).OnComplete(() => { SceneManager.LoadScene(1); });
        }
	}

}
