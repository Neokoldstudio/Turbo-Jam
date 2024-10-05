using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class SfxManager : MonoBehaviour
{
    public List<sfx> sounds = new List<sfx>();

    AudioSource source;

    public void Start()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void PlaySound(string sfxName)
    {
        if (ContainSound(sfxName))
        {
            print(sounds[GetSoundIndex(sfxName)].clips[0]);
            source.PlayOneShot(sounds[GetSoundIndex(sfxName)].clips[0]);
        }
        else
        {
            Debug.LogWarning("No Sound Associated with : " + name);
        }
    }

    bool ContainSound(string name)
    {
        for(int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == name)
                return true;
        }
        return false;
    }
    int GetSoundIndex(string name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == name)
                return i;
        }
        return -1;
    }

}

[System.Serializable]
public class sfx{

    public string name;
    public AudioClip[] clips;
}
