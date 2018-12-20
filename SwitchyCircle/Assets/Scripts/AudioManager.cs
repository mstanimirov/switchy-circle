using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    void Awake()
    {

        foreach (Sound s in sounds) {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }

    }

    void Start () {


	}

    public void PlaySound(string name) {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) {

            if (PlayerPrefs.GetInt("sound") == 0) {

                s.source.Play();

            }

        }

    }

    public void StopSound(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            s.source.Stop();

        }

    }

    public void SetSoundVolume(string name, float volume)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            s.source.volume = volume;

        }

    }

    public bool IsSoundPlaying(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            return s.source.isPlaying;

        }

        return false;

    }

}
