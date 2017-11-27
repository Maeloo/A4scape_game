using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Target : MonoBehaviour
{
    public void CrashComplete()
    {
        GetComponent<SpriteRenderer>().DOFade(0f, 1f).OnComplete(() => { Destroy(transform.parent.gameObject); });
    }
}
