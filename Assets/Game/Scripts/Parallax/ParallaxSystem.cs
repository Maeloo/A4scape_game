using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ParallaxLayer
{
    public GameObject LayerContainer;
    public int LayerOrder;
    public float LayerSpeed;

    public PoolableSprite[] ElementPrefabs;

    public float Frequency;
    public float RandomFrequencyDeviation;

    public float Height;
    public float RandomHeightDeviation;

    public bool SpawnRandomly;

    public void Init()
    {
        m_elementPools = new List<Pool<PoolableSprite>>();
        foreach (PoolableSprite element in ElementPrefabs)
        {
            m_elementPools.Add(new Pool<PoolableSprite>(element, 5, 5, Pool<PoolableSprite>.EPoolType.GameObject));
        }

        LastIndex = 0;
    }

    private List<Pool<PoolableSprite>> m_elementPools;

    private int LastIndex;
    public Transform SpawnElement()
    {
        PoolableSprite NextElement;
        if (SpawnRandomly)
        {
            LastIndex = Random.Range(0, m_elementPools.Count);
        } else
        {
            LastIndex++;
        }
            
        if (m_elementPools[LastIndex].GetAvailable(false, out NextElement))
        {
            NextElement.transform.SetParent(LayerContainer.transform);
            //Vector3 NextPos = LastTile.transform.position;
            //NextPos.x += LastTile.Renderer.bounds.extents.x + NextTile.Renderer.bounds.extents.x;
            //NextTile.transform.position = NextPos;

            //LastTile = NextTile;
            //ActiveTiles.Add(NextTile);
        }

        return null;
    }
}

public class ParallaxSystem : Singleton<ParallaxSystem>
{

    public List<ParallaxLayer> Layers;


    private void Start()
    {
        foreach (ParallaxLayer Layer in Layers)
        {
            Layer.Init();
        }
    }

    private void Update()
    {
        
    }
}
