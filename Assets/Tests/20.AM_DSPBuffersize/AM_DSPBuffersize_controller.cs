using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AM_DSPBuffersize_controller : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField]
    Text sliderValue, sliderEvaluation;
    [SerializeField]
    Slider DSPSlider;
#pragma warning restore 0649

    // Update is called once per frame
    void Update () {
        sliderValue.text = "" + DSPSlider.value;
        if (DSPSlider.value < 1024)
            if (DSPSlider.value < 512)
                sliderEvaluation.text = "Best Latency";
            else sliderEvaluation.text = "Good Latency";
        else sliderEvaluation.text = "Best Performance";
	}

    public void changeGlobalVolume(Slider slider)
    {
        AudioConfiguration config = AudioSettings.GetConfiguration();
        config.dspBufferSize = (int)slider.value;
        AudioSettings.Reset(config);
    }

    public void PlayClick()
    {
        GetComponent<AudioSource>().Play();
    }

    public void StopClick()
    {
        GetComponent<AudioSource>().Stop();
    }

}
