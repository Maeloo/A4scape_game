using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : Singleton<MeteorSpawner>
{

    [SerializeField]
    protected GameObject MeteorPrefab;
    [SerializeField]
    protected Transform MeteorParent;

    public float OffsetRange;
    public float Frequency;
    public float RandomDeviation;

    protected float CooldownSpawn;


    private void Start()
    {
        CooldownSpawn = 1f;
    }

    private void Update()
    {
        CooldownSpawn -= Time.deltaTime;

        if (CooldownSpawn <= 0f)
        {
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        float Position_X = GameCore.Instance.Player.transform.position.x + OffsetRange * .5f + Random.Range(-OffsetRange, OffsetRange);

        Instantiate<GameObject>(MeteorPrefab, new Vector3(Position_X, -2f), Quaternion.identity, MeteorParent).GetComponent<Meteor>().TriggerFall();

        CooldownSpawn = Frequency + Random.Range(-RandomDeviation, RandomDeviation);
    }
}
