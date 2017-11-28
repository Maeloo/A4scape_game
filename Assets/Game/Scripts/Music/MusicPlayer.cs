using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPlayer : MonoBehaviour
{

    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioClip Clip3;
    public AudioClip Clip4;

    protected AudioSource AS;


	// Use this for initialization
	void Start ()
    {
        AS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AS.Stop();
            AS.clip = Clip1;
            AS.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AS.Stop();
            AS.clip = Clip2;
            AS.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AS.Stop();
            AS.clip = Clip3;
            AS.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AS.Stop();
            AS.clip = Clip4;
            AS.Play();
        }
    }
}
