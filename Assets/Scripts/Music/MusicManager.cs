using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeToSwitch;
    [SerializeField] AudioClip playOnStart;

    private void Start() {
        Play(playOnStart, true);
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {

        if(musicToPlay == null){ return;}
        if (interrupt == true)
        {
            audioSource.volume = 0.03f;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }else{
            switchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    AudioClip switchTo;
    float volume;
    IEnumerator SmoothSwitchMusic()
    {
        volume = 0.03f;
        while(volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch; 
            if(volume < 0f){ volume = 0f;}
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }


}
