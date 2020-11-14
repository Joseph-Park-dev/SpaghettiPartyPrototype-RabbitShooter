using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _source;
    
    [Range(0f, 1f)]
    [SerializeField] private float _volume;

    public string Name { get { return _name; } set { _name = value; } }
    public AudioSource Source { get { return _source; } set { _source = value; } }
    public AudioClip Clip { get { return _clip; } set { _clip = value; } }
    public float Volume { get { return _volume; } set { _volume = value; } }
}
