using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BypassListenerEffects_Controller : MonoBehaviour
{

    AudioSource source;
    [SerializeField]
    Text status;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (source.bypassListenerEffects)
        {
            status.text = "Bypass Effects: ENABLED";
        }
        else
        {
            status.text = "Bypass Effects: DISABLED";
        }
    }

    public void ToggleBypass()
    {
        source.bypassListenerEffects = !source.bypassListenerEffects;
    }
}
