using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : Singleton<BirdSpawner>
{

    public float Frequency = 15f;
    public float RandomDeviation = 4f;
    public float RandomHeightOffset = 1.5f;

    public GameObject BirdPrefab;

    public Transform BirdParent;

    protected float _lastSpawnTime; //= 30f;


	void Update ()
    {
        _lastSpawnTime -= Time.deltaTime;

		if (_lastSpawnTime <= 0f)
        {
            SpawnBird();
        }
	}
    
    void SpawnBird()
    {
        Instantiate(BirdPrefab, new Vector3(GameCore.Instance.Player.transform.position.x + 40f, Random.Range(-RandomHeightOffset, RandomHeightOffset), 0f), Quaternion.identity).transform.SetParent(BirdParent);

        _lastSpawnTime = Frequency + Random.Range(-RandomDeviation, RandomDeviation);
    }
   
}
