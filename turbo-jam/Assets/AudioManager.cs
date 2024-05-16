using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Sources ------")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------ Audio clips ------")]
    [SerializeField] AudioClip currentBgMusic;



    // Start is called before the first frame update
    void Start()
    {
        MusicSource.clip = currentBgMusic;
        MusicSource.Play();
    }
}
