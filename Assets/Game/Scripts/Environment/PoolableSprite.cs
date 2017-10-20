using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableSprite : MonoBehaviour, Poolable<PoolableSprite>
{

    public SpriteRenderer Renderer { get { return m_renderer; } }
    protected SpriteRenderer m_renderer;

    private bool bUsed;


    public void Initialize(object[] args = null)
    {
        gameObject.name = "poolable_sprite";
        m_renderer = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(9999f, 9999f, 9999f);
        bUsed = false;
    }

    public bool IsReady()
    {
        return !bUsed;
    }

    public void Duplicate(PoolableSprite a_template) { }
    public void Register(Pool<PoolableSprite> pool) { }
    public void Release() { bUsed = false; }
    public void Pick() { bUsed = true; }

}
