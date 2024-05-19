using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moosicManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] AudioClip[] musics;


    private void Awake()
    {
        int rand = Random.Range(0, musics.Length);
        MusicSource.clip = musics[rand];
        MusicSource.Play();
    }

}
