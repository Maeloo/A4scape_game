using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{

    public void OnThrowEvent()
    {
        GameCore.Instance.Player.ThrowComponent.ThrowProjectile();
    }

}
