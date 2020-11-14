using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Sound[] _sounds;

    private void Awake()
    {
        foreach(Sound s in _sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
        }
    }

    private void Start()
    {
        FindObjectOfType<SoundManager>().Play("ChaseMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.Name == name);
        s.Source.Play();
    }
}
