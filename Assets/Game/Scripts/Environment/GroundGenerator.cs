using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : Singleton<MonoBehaviour>
{

    [SerializeField]
    protected PoolableSprite[] TilePrefabs;

    protected List<Pool<PoolableSprite>> TilePools;
    protected List<PoolableSprite> ActiveTiles;
    protected PoolableSprite LastTile;
    protected Vector3 ScreenPosition;


    private void Start()
    {
        TilePools = new List<Pool<PoolableSprite>>();
        ActiveTiles = new List<PoolableSprite>();

        foreach (PoolableSprite Tile in TilePrefabs)
        {
            TilePools.Add(new Pool<PoolableSprite>(Tile, 10, 10, Pool<PoolableSprite>.EPoolType.GameObject));
        }

        int rand_idx = Random.Range(0, TilePools.Count);
        PoolableSprite FirstTile;
        if (TilePools[rand_idx].GetAvailable(false, out FirstTile))
        {
            FirstTile.transform.SetParent(transform);
            FirstTile.transform.localPosition = Vector3.zero;

            LastTile = FirstTile;
            ActiveTiles.Add(FirstTile);
        }

        ScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(transform.position);

        // Spawn the first tiles on the right
        bool bContinue = true;
        do
        {
            SpawnGroundTile();

            Vector3 NextTileScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(LastTile.transform.position);
            bContinue = NextTileScreenPosition.x - ScreenPosition.x < Screen.width * 1.3333f;
        }
        while (bContinue);
    }

    void SpawnGroundTile()
    {
        PoolableSprite NextTile;
        int rand_idx = Random.Range(0, TilePools.Count);
        if (TilePools[rand_idx].GetAvailable(false, out NextTile))
        {
            NextTile.transform.SetParent(transform);
            Vector3 NextPos = LastTile.transform.position;
            NextPos.x += LastTile.Renderer.bounds.extents.x + NextTile.Renderer.bounds.extents.x;
            NextTile.transform.position = NextPos;
            
            LastTile = NextTile;
            ActiveTiles.Add(NextTile);
        }
    }

	void Update ()
    {
        Vector3 LastTileScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(LastTile.transform.position);
        Vector3 PlayerScreenPosition = GameCore.Instance.GameCamera.WorldToScreenPoint(GameCore.Instance.Player.transform.position);

        if (LastTileScreenPosition.x - PlayerScreenPosition.x < Screen.width * 0.3333f)
        {
            SpawnGroundTile();

            ActiveTiles[0].Release();
            ActiveTiles.RemoveAt(0);
        }
	}
}
