using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    
    public void OnHoverPlaySound()
    {
        audioSource.pitch = Random.Range(0.68f, 0.7f);
        audioSource.PlayOneShot(hoverSound);
    }
}
