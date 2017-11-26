using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CyclistSpawner : Singleton<CyclistSpawner>
{

    [SerializeField]
    protected GameObject CyclistPrefab;
    [SerializeField]
    protected Transform CyclistParent;

    public float Frequency = 60f;
    public float RandomDeviation = 20f;

    protected float Cooldown = 0f;
	
	
	void Update ()
    {
        Cooldown -= Time.deltaTime;
        if (Cooldown <= 0f)
        {
            Cooldown = Frequency + Random.Range(-RandomDeviation, RandomDeviation);

            Instantiate(CyclistPrefab, GameCore.Instance.Player.transform.position + new Vector3(40f, -.5f, 0f), Quaternion.identity, CyclistParent);
        }
	}
}
