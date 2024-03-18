using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField] AudioClip questCompleteSfx;
    [SerializeField] AudioClip finalVictorySound;
    [SerializeField] AudioClip happyEndingSfx;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayBackgroundSound()
    {
        audioSource.Play();
    }

    public void PlayQuestCompleteSound()
    {
        audioSource.PlayOneShot(questCompleteSfx);
    }

    public void PlayFinalVictorySoudn()
    {
        audioSource.PlayOneShot(finalVictorySound);
    }
   
    public void PlayHappyEndingSound()
    {
       audioSource.clip = happyEndingSfx;
       audioSource.Play();
    }
}
