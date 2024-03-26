using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject musicButton;
    public Sprite unmuted;
    public Sprite muted;
    public Slider volumeSlider;


    void Start()
    {
        musicSource.Play();
        
    }
    private void Update()
    {
        musicSource.volume = volumeSlider.value;
    }
    public void toggleMute()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            musicButton.GetComponent<UnityEngine.UI.Image>().sprite = unmuted;
        }
        else
        {
            musicSource.Play();
            musicButton.GetComponent<UnityEngine.UI.Image>().sprite = muted;
        }
    }

}
