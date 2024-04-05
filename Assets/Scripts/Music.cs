using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject muteButton;
    public Sprite unmuted;
    public Sprite muted;
    private Slider volumeSlider;


    void Start()
    {
        if (volumeSlider == null)
        {
            volumeSlider = GameObject.FindGameObjectWithTag("Volume Slider").GetComponent<Slider>();
        }
        DontDestroyOnLoad(gameObject);
        musicSource.Play();
    }

    private void Update()
    {
        if (volumeSlider == null) volumeSlider = GameObject.FindGameObjectWithTag("Volume Slider").GetComponentInChildren<Slider>();

        if (volumeSlider != null)
        {
            musicSource.volume = volumeSlider.value;
        }
    }
    public void toggleMute()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            muteButton.GetComponent<UnityEngine.UI.Image>().sprite = unmuted;
        }
        else
        {
            musicSource.Play();
            muteButton.GetComponent<UnityEngine.UI.Image>().sprite = muted;
        }
    }

}
