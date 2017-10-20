using UnityEngine;
using System.Collections.Generic;


public class Pool<T> : Object where T : Component, Poolable<T>, new()
{

    public enum EPoolType
    {
        Object,
        GameObject
    }


    T                       template;

    List<T>                 objects = new List<T>();


    public List<T>          objectsList
    {
        get { return objects; }
    }

    public uint             sizeMax;

    private uint            size = 0;
    private EPoolType       type;
    private List<object>    args;


    public Pool(T a_template, uint a_sizeInit, uint a_sizeMax, EPoolType type, List<object> args = null)
    {
        template            = a_template;
        sizeMax             = a_sizeMax;

        this.type           = type;
        this.args           = args != null ? args : new List<object>();

        for (byte idx = 0; idx < a_sizeInit; ++idx)
        {
            AddObjectToPool();
        }
    }

    private T AddObjectToPool()
    {
        T obj = null;

        switch (type)
        {
            case EPoolType.Object:
                obj = new T();
                break;

            case EPoolType.GameObject:
                obj = Instantiate(template);
                break;
        }

        obj.Initialize(args.ToArray());
        obj.Register(this);

        if (template != null)
            obj.Duplicate(template);

        objects.Add(obj);
        ++size;

        return obj;
    }

    public bool GetAvailable(bool a_forceExpand, out T obj)
    {
        obj = null;

        // Check & Remove Null Objects from the Pool
        for (byte idx = 0; idx < objects.Count; ++idx)
        {
            if (objects[idx] == null)
            {
                objects.RemoveAt(idx);
                idx--;
                size--;
            }
        }

        if (objects.Count > 0)
        {
            int idx = objects.Count - 1;
            while (obj == null)
            {
                if (idx < 0)
                    break;

                obj = objects[idx].IsReady() ? objects[idx] : null;

                idx--;
            }     
        }

        if (obj == null && ((size < sizeMax) || a_forceExpand))
        {
            obj = AddObjectToPool();
        }

        if (obj != null)
        {
            obj.Pick();
        }

        return obj != null;
    }

    public void Clear()
    {
        ReleaseAll();

        objects.Clear();
    }

    public void ReleaseAll()
    {
        for (byte idx = 0; idx < objects.Count; ++idx)
            objects[idx].Release();
    }
}
