using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class StereoPan_controller : MonoBehaviour
{

    AudioSource source;
    [SerializeField]
    Text status;
    [SerializeField]
    Slider stereoPanSlider;

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

        stereoPanSlider.value = source.panStereo;
    }

    // Update is called once per frame
    void Update()
    {
        source.panStereo = stereoPanSlider.value;
        status.text = "Stereo Pan: " + source.panStereo;
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
