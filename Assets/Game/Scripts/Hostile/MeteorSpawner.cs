using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : Singleton<MeteorSpawner>
{

    [SerializeField]
    protected Meteor MeteorPrefab;
    [SerializeField]
    protected Transform MeteorParent;

    public float OffsetRange;
    public float Frequency;
    public float RandomDeviation;

    protected float CooldownSpawn;


    private void Start()
    {
        CooldownSpawn = 0f;
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
        float Position_X = GameCore.Instance.Player.transform.position.x + Random.Range(-OffsetRange, OffsetRange);

        Meteor NewMeteor = Instantiate<Meteor>(MeteorPrefab, new Vector3(Position_X, 0f), Quaternion.identity, MeteorParent);
        NewMeteor.TriggerFall();

        CooldownSpawn = Frequency + Random.Range(-RandomDeviation, RandomDeviation);
    }
}
