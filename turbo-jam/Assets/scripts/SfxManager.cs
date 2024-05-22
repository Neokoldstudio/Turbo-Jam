using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class SfxManager : MonoBehaviour
{
    public List<sfx> sounds;

    AudioSource source;

    public void OnValidate()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        if (ContainSound(name))
        {
            source.PlayOneShot(GetSound(name).clips[Random.Range(0, GetSound(name).clips.Length)]);
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
    sfx GetSound(string name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == name)
                return sounds[i];
        }
        return null;
    }

}

[System.Serializable]
public class sfx{

    public string name;
    public AudioClip[] clips;
}
