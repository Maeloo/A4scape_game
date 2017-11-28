using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ParallaxLayer
{
    public SpriteRenderer SpriteTemplate;
    private List<SpriteRenderer> OrderedSprites;

    public float LayerSpeed;

    private float SpriteWidth;
    private float SpriteScale;


    public void Init()
    {
        //Store the size of the collider along the x axis (its length in units).
        SpriteScale = SpriteTemplate.transform.localScale.x;
        SpriteWidth = SpriteTemplate.GetComponent<BoxCollider2D>().size.x;

        int CountToSpawn = (int)(18f / SpriteWidth);
        CountToSpawn = CountToSpawn == 0 ? 1 : CountToSpawn;

        OrderedSprites = new List<SpriteRenderer>();
        OrderedSprites.Add(SpriteTemplate);
        for (int idx = 0; idx < CountToSpawn; ++idx)
        {
            OrderedSprites.Add(Object.Instantiate(SpriteTemplate));
            OrderedSprites[idx + 1].transform.SetParent(SpriteTemplate.transform.parent);
            OrderedSprites[idx + 1].transform.position = OrderedSprites[idx].transform.position + new Vector3(SpriteWidth * SpriteScale, 0f, 0f);
        }
    }

    public void Update(float Velocity)
    {
        for (int idx = 0; idx < OrderedSprites.Count; ++idx)
        {
            OrderedSprites[idx].transform.position -= new Vector3(Velocity * LayerSpeed, 0f, 0f);
        }

        if (Velocity > 0f)
        {
            if (OrderedSprites[0].transform.position.x < -SpriteWidth * SpriteScale  * .5f - 18f)
            {
                MeteorSpawner.Instance.SaveMeteorPosition();

                SpriteRenderer temp = OrderedSprites[0];
                temp.transform.position = OrderedSprites[OrderedSprites.Count - 1].transform.position + new Vector3(SpriteWidth * SpriteScale, 0f, 0f);
                OrderedSprites.RemoveAt(0);
                OrderedSprites.Add(temp);

                MeteorSpawner.Instance.LoadMeteorPosition();
            }
        }

        if (Velocity < 0f)
        {
            if (OrderedSprites[OrderedSprites.Count - 1].transform.position.x > SpriteWidth * SpriteScale * .5f + 18f)
            {
                MeteorSpawner.Instance.SaveMeteorPosition();

                SpriteRenderer temp = OrderedSprites[OrderedSprites.Count - 1];
                temp.transform.position = OrderedSprites[0].transform.position - new Vector3(SpriteWidth * SpriteScale, 0f, 0f);
                OrderedSprites.RemoveAt(OrderedSprites.Count - 1);
                OrderedSprites.Insert(0, temp);

                MeteorSpawner.Instance.LoadMeteorPosition();
            }
        }
    }
}

public class ParallaxSystem : Singleton<ParallaxSystem>
{

    public List<ParallaxLayer> Layers;

    private Gojira[] Gojiras;


    private void Start()
    {
        foreach (ParallaxLayer layer in Layers)
        {
            layer.Init();
        }

        Gojiras = FindObjectsOfType<Gojira>();
    }

    public void UpdateLayers(float Offset)
    {
        foreach (ParallaxLayer layer in Layers)
        {
            layer.Update(Offset);
        }

        foreach (Gojira gojira in Gojiras)
        {
            gojira.OnCameraMovement(Offset);
        }
    }
}
