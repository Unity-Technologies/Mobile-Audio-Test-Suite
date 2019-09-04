using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Pitch_controller : MonoBehaviour
{

    AudioSource source;
    [SerializeField]
    Text status;
    [SerializeField]
    Slider pitchSlider;

    FrameworkController controller;

    // Use this for initialization
    void Start()
    {
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

        pitchSlider.value = source.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        source.pitch = pitchSlider.value;
        status.text = "Pitch: " + source.pitch;
    }

    public void PlayClick()
    {
        source.Play();
    }

    public void StopClick()
    {
        source.Stop();
    }
}
