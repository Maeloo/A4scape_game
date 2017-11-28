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

    List<Meteor> _meteors;

    protected bool bActive;


    private void Start()
    {
        CooldownSpawn = 1f;

        _meteors = new List<Meteor>();

        bActive = true;
    }

    private void Update()
    {
        if (!bActive)
        {
            return;
        }

        CooldownSpawn -= Time.deltaTime;

        if (CooldownSpawn <= 0f)
        {
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        float Position_X = GameCore.Instance.Player.transform.position.x + OffsetRange * .5f + Random.Range(-OffsetRange, OffsetRange);

        Meteor newMeteor = Instantiate<GameObject>(MeteorPrefab, new Vector3(Position_X, -2f), Quaternion.identity, MeteorParent).GetComponent<Meteor>();
        newMeteor.TriggerFall();

        _meteors.Add(newMeteor);

        CooldownSpawn = Frequency + Random.Range(-RandomDeviation, RandomDeviation);
    }

    public void SaveMeteorPosition()
    {
        foreach (Meteor m in _meteors)
        {
            if (m != null)
            {
                m._savedPosition = m.transform.position;
            }
        }
    }

    public void LoadMeteorPosition()
    {
        foreach (Meteor m in _meteors)
        {
            if (m != null)
            {
                m.transform.position = m._savedPosition;
            }
        }
    }

    public void Stop()
    {
        bActive = false;

        foreach (Meteor m in _meteors)
        {
            if (m != null)
            {
                m.StopFall();
            }
        }
    }
}
