using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] AudioSource MusicSource;

    [SerializeField] AudioClip[] musics;


    private void Awake()
    {
        int rand = Random.Range(0, musics.Length);
        MusicSource.clip = musics[rand];
        MusicSource.Play();
    }

}
