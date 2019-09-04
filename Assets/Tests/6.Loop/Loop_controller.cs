using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Loop_controller : MonoBehaviour
{

    AudioSource source;
    [SerializeField]
    Text status;
    [SerializeField]
    Slider audioProgress;

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

        audioProgress.maxValue = source.clip.length;
        audioProgress.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (source.loop)
        {
            status.text = "Loop: ENABLED";
        }
        else
        {
            status.text = "Loop: DISABLED";
        }
        audioProgress.value = source.time;
    }

    public void ToggleLoop()
    {
        source.loop = !source.loop;
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
