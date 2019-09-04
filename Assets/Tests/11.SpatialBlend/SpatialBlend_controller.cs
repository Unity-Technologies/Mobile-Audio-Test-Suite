using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SpatialBlend_controller : MonoBehaviour
{

    AudioSource source;
    [SerializeField]
    Text status;
    [SerializeField]
    Slider spatialBlendSlider;
    [SerializeField]
    GameObject target;
    [SerializeField]
    float speed = 1;

    private int x = 0;
    FrameworkController controller;

    // Use this for initialization
    void Start()
    {
        controller = FindObjectOfType<FrameworkController>();
        source = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");
        if (controller != null)
        {
            source.Play();
        }
        else
        {
            Debug.Log("Framework controller not found. Are you starting from MainMenu Scene?");
        }

        spatialBlendSlider.value = source.spatialBlend;
    }

    // Update is called once per frame
    void Update()
    {
        x++;
        if (x > 360)
            x = 0;
        transform.position = new Vector3(target.transform.position.x + Mathf.Sin(Mathf.Deg2Rad * x * speed), target.transform.position.y, target.transform.position.z + Mathf.Cos(Mathf.Deg2Rad * x * speed));
        source.spatialBlend = spatialBlendSlider.value;
        status.text = "Spatial Blend: " + source.spatialBlend;

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
