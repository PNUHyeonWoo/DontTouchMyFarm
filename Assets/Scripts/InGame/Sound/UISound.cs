using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public static UISound uiSound;
    [SerializeField]
    private AudioClip[] sounds;

    public void Awake()
    {
        uiSound = this;
    }

    public void PlaySound(int index)
    { 
        GetComponent<AudioSource>().clip = sounds[index];
        GetComponent<AudioSource>().Play();
    }
}
