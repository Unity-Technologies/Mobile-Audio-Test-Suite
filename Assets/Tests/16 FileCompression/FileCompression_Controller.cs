using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class FileCompression_Controller : MonoBehaviour {

    [System.Serializable]
    public class AudioWithFormat
    {
        [SerializeField]
        private AudioClip audio;
        [SerializeField]
        private string format;

        public AudioClip GetAudio()
        {
            return audio;
        }

        public void SetAudio(AudioClip value)
        {
            audio = value;
        }

        public string GetFormat()
        {
            return format;
        }

        public void SetFormat(string value)
        {
            format = value;
        }
    }

#pragma warning disable 0649
    [SerializeField]
    Dropdown nameDD, compressionDD;
#pragma warning restore 0649
#pragma warning disable 0414
    [SerializeField]
    private int trackIndex = 0;
#pragma warning restore 0414
    public List<AudioWithFormat> audioFiles;
    private AudioClip chosenAudio;
    private AudioSource source;
    public Text chosenAudioStatus;
    public Text trackIDText;

    int NameAmount;
    int CompressionAmount;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.Play();

        nameDD.onValueChanged.AddListener(delegate
        {
            UpdateChosenAudioFromList();
        });
        compressionDD.onValueChanged.AddListener(delegate
        {
            UpdateChosenAudioFromList();
        });

        NameAmount = nameDD.options.Count;
        CompressionAmount = compressionDD.options.Count;

        UpdateChosenAudioFromList();
    }

    private void UpdateChosenAudioFromList()
    {

        string nameString = "";
        string compressionString = "";

        nameString = nameDD.options[nameDD.value].text;
        compressionString = compressionDD.options[compressionDD.value].text;

        chosenAudio = null;

        // Go through all the audio files. Convert the names and formats to lower case and check against the lowercase variants 
        // of the dropdown values. If all 3 conditions are met, select that Audio file for testing
        for (int i = 0; i < audioFiles.Count; i++)
        {
            if (audioFiles[i].GetAudio().name.ToLower().Contains(nameString.ToLower()))
            {
                //Debug.Log("Contains Name");
                if (audioFiles[i].GetAudio().name.ToLower().Contains(compressionString.ToLower()))
                {
                    chosenAudio = audioFiles[i].GetAudio();
                    chosenAudioStatus.text = chosenAudio.name + ".mp3";
                }
            }
        }
        if (chosenAudio == null)
        {
            chosenAudioStatus.text = "Audio not found";
        }
        else
        {
            source.clip = chosenAudio;
            source.Play();
        }
        trackIndex = UpdateIDByDropdowns();
    }

    void Update()
    {
        trackIDText.text = (trackIndex + 1) + "/" + (NameAmount * CompressionAmount);
    }

    public void PrevClick()
    {
        if (trackIndex > 0)
            trackIndex--;
        else trackIndex = NameAmount * CompressionAmount - 1;
        UpdateDropdownsByID(trackIndex);
    }

    public void NextClick()
    {

        if (trackIndex < (NameAmount * CompressionAmount - 1))
            trackIndex++;
        else trackIndex = 0;
        UpdateDropdownsByID(trackIndex);
    }

    public void StopClick()
    {
        source.Stop();
    }

    public void PlayClick()
    {
        source.Play();
    }

    public void UpdateDropdownsByID(int id)
    {
        nameDD.value = (id / (CompressionAmount)) % NameAmount;
        compressionDD.value = id % CompressionAmount;
    }

    public int UpdateIDByDropdowns()
    {
        int ID = nameDD.value * (CompressionAmount) + compressionDD.value;
        return ID;
    }
}
