using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup ambienceMixer;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup voicesMixer;


    private void Awake()
    {
        if (instance == null)
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
    }
}
