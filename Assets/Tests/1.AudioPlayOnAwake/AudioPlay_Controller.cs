using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlay_Controller : MonoBehaviour {

    FrameworkController controller;

	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<FrameworkController>();

        if (controller != null)
        {
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
        }
        else
        {
            Debug.Log("Framework controller not found. Are you starting from MainMenu Scene?");
        }
	}


}
