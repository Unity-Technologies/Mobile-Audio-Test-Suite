using UnityEngine;
using UnityEngine.UI;
// The code example shows how to implement a metronome that procedurally
// generates the click sounds via the OnAudioFilterRead callback.
// While the game is paused or suspended, this time will not be updated and sounds
// playing will be paused. Therefore developers of music scheduling routines do not have
// to do any rescheduling after the app is unpaused

[RequireComponent(typeof(AudioSource))]
public class OnAudioFilterRead_controller : MonoBehaviour
{
    public double bpm = 140.0F;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
#pragma warning disable 0649
    [SerializeField]
    Slider sliderBPM, sliderGAIN, sliderSigHi, sliderSigLo;
    [SerializeField]
    Text valueBPM, valueGAIN, valueSigHi, valueSigLo;
#pragma warning restore 0649

    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;

    void Start()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;
    }

    void Update()
    {
        bpm = sliderBPM.value;
        gain = sliderGAIN.value;
        signatureHi = (int)sliderSigHi.value;
        signatureLo = (int)sliderSigLo.value;

        valueBPM.text = "" + sliderBPM.value;
        valueGAIN.text = "" + sliderGAIN.value;
        valueSigHi.text = "" + sliderSigHi.value;
        valueSigLo.text = "" + sliderSigLo.value;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;
        double samplesPerTick;
        if (signatureLo > 0 && bpm > 0)
        {
            samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        }
        else samplesPerTick = sampleRate * 60.0F * 4.0F;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
                //Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }
}