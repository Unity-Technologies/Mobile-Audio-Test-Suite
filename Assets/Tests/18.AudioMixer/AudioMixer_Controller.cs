using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixer_Controller : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    AudioMixerGroup master, group1, group2;
    [SerializeField]
    AudioMixerSnapshot snapshot1, snapshot2;
    [SerializeField]
    Slider wetmix, drymix, bangVolume;
    [SerializeField]
    Text wetmixValue, drymixValue, bangVolumeValue;
    [SerializeField]
    Text wetmixStatus, drymixStatus, bangVolumeStatus;
    [SerializeField]
    Button Button1, Button2;
#pragma warning restore 0649

    public void TransitionToSnapshot1()
    {
        snapshot1.TransitionTo(2);
        Button1.image.color = Color.green;
        Button2.image.color = Color.white;
    }

    public void TransitionToSnapshot2()
    {
        snapshot2.TransitionTo(2);
        Button2.image.color = Color.green;
        Button1.image.color = Color.white;
        
    }

    void Start ()
    {
        UpdateSliders();
        Button1.image.color = Color.green;
    }
    void UpdateSliders()
    {
        float value;
        mixer.GetFloat("WetmixFlange", out value);
        wetmix.value = value;
        mixer.GetFloat("DrymixFlange", out value);
        drymix.value = value;
        mixer.GetFloat("BangVolume", out value);
        bangVolume.value = value;
    }
    void Update ()
    {
        wetmixValue.text = wetmix.value * 100 + " % ";
        drymixValue.text = drymix.value * 100 + " % ";
        bangVolumeValue.text = bangVolume.value + " dB ";
    }

    public void setWetmix()
    {
        mixer.SetFloat("WetmixFlange", wetmix.value);
        wetmixStatus.text = "SET";
    }

    public void setDrymix()
    {
        mixer.SetFloat("DrymixFlange", drymix.value);
        drymixStatus.text = "SET";
    }

    public void setVolume()
    {
        mixer.SetFloat("BangVolume", bangVolume.value);
        bangVolumeStatus.text = "SET";
    }

    public void ClearValues()
    {
        mixer.ClearFloat("WetmixFlange");
        wetmixStatus.text = "_";
        mixer.ClearFloat("DrymixFlange");
        drymixStatus.text = "_";
        mixer.ClearFloat("BangVolume");
        bangVolumeStatus.text = "_";
    }
}
