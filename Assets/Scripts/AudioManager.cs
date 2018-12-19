using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

// Source https://www.youtube.com/watch?v=6OT43pvUyfY for implementation
public class AudioManager : MonoBehaviour {


    public Sound[] sounds;

    public static AudioManager instance;

	// Use this for initialization
	void Awake ()
    {
        //The same audiomanager should exist over multiple scene transitions. Using the singleton pattern to create one single
        //instance of the audiomanager.
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Each sound gets its attributes
		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
	}

    //The theme song will be played as soon as one starts the game
    private void Start()
    {
        PlayMusic("Theme");
    }

    //Function that gets called when you want to trigger a specific music or soundeffect
    public void PlayMusic(string name)
    {
        //Searches for the wanted sound and plays it at the end
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        s.source.Play();
    }

    //Function that stops the music given by the parameter
     public void StopMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        s.source.Stop();
    }

    //Function that serch the array list for a specific sound and returns that one
    public Sound FindSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
  
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            
        }
       return s;
    }

    // Turns the volume of the selected sound over time down until it cannot be heard anymore
    public void FadeOut(Sound sound)
    {

        Sound s = sound;
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        if(s.source.volume>0)
        {
            s.source.volume -= 0.01f;
        }
    }
}
