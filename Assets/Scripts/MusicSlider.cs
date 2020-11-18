using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MusicSlider : MonoBehaviour
{
    public UnityEngine.UI.Slider volumeSlider;
    public GameObject music;

    // Update is called once per frame
    void Update()
    {
        var val = volumeSlider.value;
        music.GetComponent<AudioSource>().volume = val;
    }
}
