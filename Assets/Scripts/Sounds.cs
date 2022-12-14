using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public Sound[] sounds;
    protected virtual void Start()
    {
        foreach (Sound s in sounds)
            AddSound(s);
    }
    public void AddSound(Sound s)
    {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        
        // For distance
        s.source.maxDistance = 10;
        s.source.rolloffMode = AudioRolloffMode.Linear;
        s.source.spatialBlend = s.spacialBlend;

    }
    public void StopAudio(Sound sound)
    {
        // Stop audio
        sound.source.Stop();
        //StartCoroutine(FadeAudio(s));
    }
    IEnumerator FadeAudio(Sound sound)
    {
        // Store default volume
        float volumeDefault = sound.source.volume;

        for (int i = 0; i < 10; i++)
        {
            // Lower the volume
            sound.source.volume -= volumeDefault / 10;
            yield return new WaitForSeconds(0.05f);
        }
        // Disable source
        sound.source.Stop();
        
        // Then set volume back to default
        sound.source.volume = volumeDefault;
    }
    public void PlayAudio(Sound sound, bool interrupt = false)
    {
        if (sound == null)
            return;

        if (interrupt == true)
        {
            sound.source.Play();
            return;
        }    

        // Play audio
        if (sound.source.isPlaying == false)
            sound.source.Play();
    }
    public void PlayAudio (string name, bool interrupt = false)
    {
        Sound sound = GetSound (name);

        if (sound == null)
            return;

        if (interrupt == true)
        {
            sound.source.Play();
            return;
        }    

        // Play audio
        if (sound.source.isPlaying == false)
            sound.source.Play();
    }
    Sound GetSound(string name)
    {
        // Search for sound
        Sound s = null;
        foreach (Sound temp in sounds)
        {  
            if (temp.name == name)
                s = temp;
        }

        // If didn't find clip
        if (s == null) {
            Debug.Log("Couldn't find sound clip: " + name);
            return null;
        }

        // Return the sound
        return s;
    }
}
