using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource musicSource;
    // change button source image to reflect current state
    public GameObject musicButton;
    public Sprite unmuted;
    public Sprite muted;
    // Start is called before the first frame update


    void Start()
    {
        musicSource.Play();
        
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
