using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] AudioClip[] musics;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        int rand = Random.Range(0, musics.Length);
        MusicSource.clip = musics[rand];
        MusicSource.Play();
    }

    public void SFXplayer(AudioClip audioClip, Transform transform, float volume)
    {
        SFXSource.clip = audioClip;
        SFXSource.volume = volume;
        SFXSource.Play();
    }
    public void PlaySFXClip(AudioClip audioClip, Transform SoundTransform, float volume)
    {
        // spawn game object 
        AudioSource audioSource = Instantiate(SFXSource, SoundTransform.position, Quaternion.identity);

        // Assign audioClip 
        audioSource.clip = audioClip;

        //asign volume
        audioSource.volume = volume;

        // play sound
        audioSource.Play();

        // get length of SFX clip 
        float cliplength = audioSource.clip.length;

        // Destroy game object
        Destroy(audioSource.gameObject, cliplength);

    }
}
