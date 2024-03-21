using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    [SerializeField] private AudioMixerGroup audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        Play("MainMenu");
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("No sound file found!");
        }
        s.source.outputAudioMixerGroup = audioMixer;
        s.source.Play();
    }

    public void IncreaseVolumeGradually(string name)
    {
        /*Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("No sound file found!");
        }
        s.source.volume += 0.1f;*/
    }

    public void ResetVolume(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("No sound file found!");
        }
        s.source.volume = 0f;
    }
}
