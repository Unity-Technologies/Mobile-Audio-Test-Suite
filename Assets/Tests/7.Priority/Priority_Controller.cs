using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Priority_Controller : MonoBehaviour
{

    AudioSource source;
    [SerializeField]
#pragma warning disable 0649
    Text soundPriorityStatus, noisePriorityStatus;
    [SerializeField]
    Slider SoundSlider, NoiseSlider;
#pragma warning restore 0649
    [SerializeField]
    GameObject Filler;
    FrameworkController controller;

    // Use this for initialization
    void Start()
    {
        AudioConfiguration config = AudioSettings.GetConfiguration();
        config.numVirtualVoices = 6;
        config.numRealVoices = 2;
        AudioSettings.Reset(config);

        controller = FindObjectOfType<FrameworkController>();
        source = GetComponent<AudioSource>();

        if (controller != null)
        {
            source.Play();
        }
        else
        {
            Debug.Log("Framework controller not found. Are you starting from MainMenu Scene?");
        }

        SoundSlider.onValueChanged.AddListener( delegate {
            UpdatePriorities();
            });
        NoiseSlider.onValueChanged.AddListener(delegate {
            UpdatePriorities();
        });

        UpdatePriorities();
        PlayAll();
    }

    void UpdatePriorities()
    {
        source.priority = (int)SoundSlider.value;
        AudioSource[] fillers = Filler.GetComponentsInChildren<AudioSource>();
        for (int i = 0; i < fillers.Length; i++)
        {
            fillers[i].priority = (int)NoiseSlider.value;
        }
    }

    void Update()
    {
        soundPriorityStatus.text = "Sound Priority: " + SoundSlider.value;
        noisePriorityStatus.text = "Noise Priority: " + NoiseSlider.value;
    }

    public void PlayAll()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        if (sources == null)
            return;
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].Play();
        }
    }

    public void StopAll()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        if (sources == null)
            return;
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].Stop();
        }
    }
}