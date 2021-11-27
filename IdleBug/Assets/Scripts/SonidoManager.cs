using UnityEngine.Audio;
using System;
using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup mixer;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
public class SonidoManager : MonoBehaviour
{
    private static SonidoManager _instance;

    public static SonidoManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SonidoManager>();
            }

            return _instance;
        }
    }

    public Sound[] sonidos;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sonidos)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = s.mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }

    }

    private void Start()
    {
     
        CargarVolumenGuardado();
    }
    public void CargarVolumenGuardado()
    {
        if (audioMixer != null) audioMixer.SetFloat("SFXVolume", Mathf.Log10(SfxVolume) * 20);
        if (audioMixer != null) audioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolume) * 20);
    }
    public void Restart()
    {
        foreach (Sound s in sonidos)
        {
            Stop(s.name);

        }
    }
    public bool IsPlaying(string name)
    {
        bool playing = false;
        Sound s = Array.Find(sonidos, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " null");
            return playing;
        }
        if (s.source != null)
        {
            if (s.source.isPlaying && s.source.clip == s.clip)
            {
                playing = true;
            }
        }
        return playing;
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sonidos, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " null");
            return;
        }
        if (s.source != null) s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sonidos, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "null");
            return;
        }
        if (s.source != null) s.source.Stop();
    }
    //public void Change(string name)
    //{
    //    Sound s = Array.Find(sounds, sound => sound.name == name);
    //    s.source.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("Master/Music/In Cables");
    //    CargarVolumenGuardado();
    //}
    public AudioMixer audioMixer;

    [Range(0, 1)]
    float musicVolume = 0.5f;

    [Range(0, 1)]
    float sfxVolume = 0.5f;

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            musicVolume = value;
            if (audioMixer != null) audioMixer.SetFloat("Musicvolume", Mathf.Log10(musicVolume) * 20);
        }
    }

    public float SfxVolume
    {
        get { return sfxVolume; }
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            sfxVolume = value;
            if (audioMixer != null) audioMixer.SetFloat("SFXvolume", Mathf.Log10(sfxVolume) * 20);
        }
    }


}

